using meetup_telegram_bot.Data;
using MeetupTelegramBot.BusinessLayer.Interfaces;
using MeetupTelegramBot.BusinessLayer.Models;
using MeetupTelegramBot.BusinessLayer.Models.DTO;
using MeetupTelegramBot.BusinessLayer.Services;
using MeetupTelegramBot.DataAccess.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace meetup_telegram_bot.Controllers
{
    [ApiController]
    [Route("api/bot")]
    public class BotController : ControllerBase
    {
        private readonly TelegramBotClient client;
        private readonly IFeedbackService _feedbackService; 
        private readonly ClientStatesService _clientStatesService;
        private readonly INotificationService _notificationService;
        private readonly IQuestionService _questionService;

        private const string TelegramBotToken = "TelegramBotToken";
        private const string AdminUserName = "AdminUserName";
        private const string AdminChatId = "AdminChatId";
        
        private readonly string adminUserName;
        private readonly long adminChatId;

        public BotController(
            IConfiguration configuration, 
            ClientStatesService clientStates,
            INotificationService notificationService,
            IFeedbackService feedbackService, IQuestionService questionService)
        {
            var token = configuration.GetSection("environmentVariables").GetValue<string>(TelegramBotToken);
            if (string.IsNullOrEmpty(token))
            {
                throw new Exception("Not found telegram bot token in configuration.");
            }
            client = new TelegramBotClient(token);

            adminUserName = configuration.GetSection("environmentVariables").GetValue<string>(AdminUserName);
            if (string.IsNullOrEmpty(adminUserName))
            {
                throw new Exception("Not found admin user name in configuration.");
            }
            
            adminChatId = configuration.GetSection("environmentVariables").GetValue<long>(AdminChatId);
            if (adminChatId == 0)
            {
                throw new Exception("Not found admin chat id in configuration.");
            }

            _clientStatesService = clientStates;
            _notificationService = notificationService;
            _feedbackService = feedbackService;
            _questionService = questionService;
        }

        #region endpoints
        [HttpPost]
        public async Task Post([FromBody] Update update)
        {
            if (update == null)
            {
                throw new ArgumentNullException(nameof(update));
            }
            if (update.Type != UpdateType.Message)
            {
                return;
            }

            await ProcessUpdateMessageAsync(update.Message).ConfigureAwait(false);
        }

        #endregion

        #region private methods
        private async Task ProcessUpdateMessageAsync(Message message)
        {
            if (_clientStatesService.ClientStates.ContainsKey(message.Chat.Id))
            {
                switch (_clientStatesService.ClientStates[message.Chat.Id].UserState)
                {
                    case UserState.LeaveFeedback:
                        if (_clientStatesService.ClientStates[message.Chat.Id].FeedbackGeneralFeedback == null)
                        {
                            await ProcessLeavingFeedback(message).ConfigureAwait(false);
                        }
                        else
                        {
                            await ProcessLeavingFeedbackFutureProposal(message).ConfigureAwait(false);
                        }
                        break;
                    case UserState.FirstPresentationQuestion:
                    case UserState.SecondPresentationQuestion:
                    case UserState.ThirdPresentationQuestion:
                    case UserState.OutOfPresentationQuestion:
                        await ProcessPresentationQuestionAsync(message).ConfigureAwait(false);
                        break;
                    default:
                        await client.SendTextMessageAsync(message.Chat.Id, $"Пожалуйста, попробуйте еще раз", replyMarkup: GetButtons()).ConfigureAwait(false);
                        break;
                }
            }
            else 
            {
                await ProcessMainKeyboardAsync(message).ConfigureAwait(false);
            }
        }
        
        private async Task ProcessLeavingFeedbackFutureProposal(Message message)
        {
            var feedback = new FeedbackDTO
            {
                Id = Guid.NewGuid(),
                Date = DateTime.Now.Date,
                FutureProposal = message.Text,
                GeneralFeedback = _clientStatesService.ClientStates[message.Chat.Id].FeedbackGeneralFeedback,
                Time = DateTime.Now.TimeOfDay,
                AuthorName = AuthorNameGenerator.Generate()
            };
            try
            {
                await _feedbackService.CreateAsync(feedback).ConfigureAwait(false);
                await _notificationService.SendFeedbackAsync(feedback).ConfigureAwait(false);
                await client.SendTextMessageAsync(message.Chat.Id, $"Спасибо, ваш фидбэк '{feedback.GeneralFeedback}' и предложения '{feedback.FutureProposal}' были сохранены под ником '{feedback.AuthorName}'", replyMarkup: GetButtons()).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                await client.SendTextMessageAsync(message.Chat.Id, $"Exception: {ex.Message}").ConfigureAwait(false);
            }
            _clientStatesService.ClientStates.Remove(message.Chat.Id);
        }
        
        private async Task ProcessLeavingFeedback(Message message)
        {
            _clientStatesService.SetFeedback(message.Chat.Id, message.Text);
            await client.SendTextMessageAsync(message.Chat.Id, "Напишите предложения для будущих митапов", replyMarkup: null).ConfigureAwait(false);
        }
        
        /// <summary>
        /// Create question model, add it to database and send message on success to user
        /// </summary>
        private async Task ProcessPresentationQuestionAsync(Message message)
        {
            _clientStatesService.SetPresentationQuestion(message.Chat.Id, message.Text);
            var clientService = _clientStatesService.ClientStates[message.Chat.Id];
            Guid presentationId;
            try
            {
                // ToDo: fetch in another method
                presentationId = clientService.UserState switch
                {
                    UserState.FirstPresentationQuestion => new Guid("fadabc27-40e4-47f3-bc1b-f0916b4772cd"),
                    UserState.SecondPresentationQuestion => new Guid("0c03ba0b-3b46-42ba-ba39-6b635c9a4bc0"),
                    UserState.ThirdPresentationQuestion => new Guid("99d09f48-0fec-4ef4-8292-2bab81de8d37"),
                    UserState.OutOfPresentationQuestion => new Guid("958AE825-56F4-4390-90E3-4AA9741673A3"),
                    UserState.LeaveFeedback => throw new Exception($"Invalid user state = {clientService.UserState}"),
                    _ => throw new Exception($"Invalid user state = {clientService.UserState}")
                };
                var question = new QuestionDTO 
                {
                    Id = Guid.NewGuid(),
                    Date = DateTime.Now.Date,
                    Text = clientService.QuestionText,
                    AuthorName = AuthorNameGenerator.Generate(),
                    PresentationId = presentationId,
                    Time = DateTime.Now.TimeOfDay
                };
                await _questionService.CreateAsync(question).ConfigureAwait(false);
                await _notificationService.SendQuestionAsync(question).ConfigureAwait(false);
                await client.SendTextMessageAsync(message.Chat.Id, $"Спасибо, ваш вопрос '{question.Text}' и никнейм '{question.AuthorName}' были сохранены", replyMarkup: GetButtons()).ConfigureAwait(false);
            }
            catch (Exception)
            {
                await client.SendTextMessageAsync(message.Chat.Id, $"Попробуйте еще раз", replyMarkup: GetButtons()).ConfigureAwait(false);
            }
            _clientStatesService.ClientStates.Remove(message.Chat.Id);
        }

        //ToDo: add back to main menu button
        private async Task ProcessMainKeyboardAsync(Message message)
        {
            const string questionText = "Пожалуйста, введите свой вопрос";
            const string feedbackText = "Оставьте свой фидбэк";

            switch (message.Text)
            {
                case MainKeyboard.FirstPresentationQuestion:
                    _clientStatesService.SetUserState(message.Chat.Id, UserState.FirstPresentationQuestion);
                    await client.SendTextMessageAsync(message.Chat.Id, questionText, replyMarkup: new ReplyKeyboardRemove()).ConfigureAwait(false);
                    break;
                case MainKeyboard.SecondPresentationQuestion:
                    _clientStatesService.SetUserState(message.Chat.Id, UserState.SecondPresentationQuestion);
                    await client.SendTextMessageAsync(message.Chat.Id, questionText, replyMarkup: new ReplyKeyboardRemove()).ConfigureAwait(false);
                    break;
                case MainKeyboard.ThirdPresentationQuestion:
                    _clientStatesService.SetUserState(message.Chat.Id, UserState.ThirdPresentationQuestion);
                    await client.SendTextMessageAsync(message.Chat.Id, questionText, replyMarkup: new ReplyKeyboardRemove()).ConfigureAwait(false);
                    break;
                case MainKeyboard.OutOfPresentationQuestion:
                    _clientStatesService.SetUserState(message.Chat.Id, UserState.OutOfPresentationQuestion);
                    await client.SendTextMessageAsync(message.Chat.Id, questionText, replyMarkup: new ReplyKeyboardRemove()).ConfigureAwait(false);
                    break;
                case MainKeyboard.LeaveFeedback:
                    _clientStatesService.SetUserState(message.Chat.Id, UserState.LeaveFeedback);
                    await client.SendTextMessageAsync(message.Chat.Id, feedbackText, replyMarkup: new ReplyKeyboardRemove()).ConfigureAwait(false);
                    break;
                default:
                    await client.SendTextMessageAsync(message.Chat.Id, "Пожалуйста, выберите пункт главного меню", replyMarkup: GetButtons()).ConfigureAwait(false);
                    break;
            }
        }

        private IReplyMarkup GetButtons()
        {
            return new ReplyKeyboardMarkup
            (
                new List<List<KeyboardButton>>
                {
                    new()
                    {
                        new KeyboardButton(text: MainKeyboard.FirstPresentationQuestion),
                        new KeyboardButton(text: MainKeyboard.SecondPresentationQuestion)
                    },
                    new()
                    {
                        new KeyboardButton(text: MainKeyboard.ThirdPresentationQuestion),
                        new KeyboardButton(text: MainKeyboard.OutOfPresentationQuestion)
                    },
                    new()
                    {
                        new KeyboardButton(text: MainKeyboard.LeaveFeedback),
                    }
                }
             );
        }
        #endregion
    }
}
