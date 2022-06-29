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

        private readonly Guid currentMeetupId;
        private readonly string adminUserName;
        private readonly long adminChatId;

        public BotController(
            IConfiguration configuration, 
            ClientStatesService clientStates,
            INotificationService notificationService,
            IFeedbackService feedbackService, IQuestionService questionService)
        {
            //ToDO: get from db?
            currentMeetupId = new Guid("7ef9eade-92a2-4277-94df-45b802157ef3");

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

            await ProcessUpdateMessageAsync(update.Message);
        }

        #endregion

        #region private methods
        private async Task ProcessUpdateMessageAsync(Message message)
        {
            var chatId = message.Chat.Id;

            if (_clientStatesService.ClientStates.ContainsKey(chatId))
            {
                var userState = _clientStatesService.ClientStates[chatId].UserState;
                if (_clientStatesService.IsFeedback(userState))
                {
                    if (_clientStatesService.ClientStates[chatId].FeedbackGeneralFeedback == null)
                    {
                        await ProcessLeavingFeedback(message);
                    }
                    else
                    {
                        await ProcessLeavingFeedbackFutureProposal(message);
                    }
                }
                else if (_clientStatesService.IsPresentation(userState))
                {
                    await ProcessPresentationQuestionAsync(message);
                }
                else
                {
                    await client.SendTextMessageAsync(chatId, $"Пожалуйста, попробуйте еще раз", replyMarkup: GetButtons());
                }
            }
            else 
            {
                await ProcessMainKeyboardAsync(message);
            }
        }
        
        private async Task ProcessLeavingFeedbackFutureProposal(Message message)
        {
            var feedback = new FeedbackDTO
            {
                Id = Guid.NewGuid(),
                //Date = DateTime.Now,
                Time = DateTime.Now.TimeOfDay,
                FutureProposal = message.Text,
                GeneralFeedback = _clientStatesService.ClientStates[message.Chat.Id].FeedbackGeneralFeedback,
                AuthorName = AuthorNameGenerator.Generate(),
                MeetupId = currentMeetupId
            };
            try
            {
                await _feedbackService.CreateAsync(feedback);
                await _notificationService.SendFeedbackAsync(feedback);
                await client.SendTextMessageAsync(message.Chat.Id, $"Спасибо, ваш фидбэк '{feedback.GeneralFeedback}' и предложения '{feedback.FutureProposal}' были сохранены под ником '{feedback.AuthorName}'", replyMarkup: GetButtons());
            }
            catch (Exception ex)
            {
                await client.SendTextMessageAsync(adminChatId, $"Exception: {ex.Message}");
            }
            _clientStatesService.ClientStates.Remove(message.Chat.Id);
        }
        
        private async Task ProcessLeavingFeedback(Message message)
        {
            _clientStatesService.SetFeedback(message.Chat.Id, message.Text);
            await client.SendTextMessageAsync(message.Chat.Id, "Напишите предложения для будущих митапов", replyMarkup: null);
        }
        
        /// <summary>
        /// Create question model, add it to database and send message on success to user
        /// </summary>
        private async Task ProcessPresentationQuestionAsync(Message message)
        {
            _clientStatesService.SetPresentationQuestion(message.Chat.Id, message.Text);
            var clientState = _clientStatesService.ClientStates[message.Chat.Id];
            try
            {
                Guid presentationId = _clientStatesService.GetPresentationId(clientState.UserState);
                var question = new QuestionDTO 
                {
                    Id = Guid.NewGuid(),
                    Time = DateTime.Now.TimeOfDay,
                    Text = clientState.QuestionText,
                    AuthorName = AuthorNameGenerator.Generate(),
                    MeetupId = currentMeetupId,
                    PresentationId = presentationId
                };
                await _questionService.CreateAsync(question);
                await _notificationService.SendQuestionAsync(question);
                await client.SendTextMessageAsync(message.Chat.Id, $"Спасибо, ваш вопрос '{question.Text}' и никнейм '{question.AuthorName}' были сохранены", replyMarkup: GetButtons());
            }
            catch (Exception)
            {
                await client.SendTextMessageAsync(message.Chat.Id, $"Попробуйте еще раз", replyMarkup: GetButtons());
            }
            _clientStatesService.ClientStates.Remove(message.Chat.Id);
        }

        //ToDo: add back to main menu button
        private async Task ProcessMainKeyboardAsync(Message message)
        {
            const string questionText = "Пожалуйста, введите свой вопрос";
            const string feedbackText = "Оставьте свой фидбэк";

            if (_clientStatesService.IsPresentation(message.Text))
            {
                _clientStatesService.SetUserState(message.Chat.Id, message.Text);
                await client.SendTextMessageAsync(message.Chat.Id, questionText, replyMarkup: new ReplyKeyboardRemove());

            }
            else if(_clientStatesService.IsFeedback(message.Text))
            {
                _clientStatesService.SetUserState(message.Chat.Id, message.Text);
                await client.SendTextMessageAsync(message.Chat.Id, feedbackText, replyMarkup: new ReplyKeyboardRemove());
            }
            else
            {
                await client.SendTextMessageAsync(message.Chat.Id, "Пожалуйста, выберите пункт главного меню", replyMarkup: GetButtons());
            }
        }

        private IReplyMarkup GetButtons()
        {
            var presentations = _clientStatesService.GetPresentations();
            
            var keyboardButtons = new List<List<KeyboardButton>>();
            
            for (var i = 0; i < presentations.Count; i += 2)
            {
                if (i + 1 == presentations.Count)//Take the last one
                {
                    keyboardButtons.Add(new()
                    {
                        new KeyboardButton(text: presentations[i]),
                        new KeyboardButton(text: ClientStatesService.LeaveFeedback),
                    });
                }
                else 
                {
                    keyboardButtons.Add(new()
                    {
                        new KeyboardButton(text: presentations[i]),
                        new KeyboardButton(text: presentations[i + 1]),
                    });
                    if (i + 2 == presentations.Count)
                    {
                        keyboardButtons.Add(new()
                        {
                            new KeyboardButton(text: ClientStatesService.LeaveFeedback),
                        });
                    }
                }
            }
            return new ReplyKeyboardMarkup
            (
                keyboardButtons
             );
        }
        #endregion
    }
}
