using meetup_telegram_bot.Data;
using meetup_telegram_bot.Data.DbEntities;
using meetup_telegram_bot.Infrastructure.Interfaces;
using meetup_telegram_bot.Services;
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
        private readonly IFeedbackRepository _feedbackRepository;
        private readonly IQuestionRepository _questionRepository;
        private readonly ClientStatesService _clientStatesService;
        private readonly INotificationService _notificationService;

        private const string TelegramBotToken = "TelegramBotToken";
        private const string AdminUserName = "AdminUserName";
        private const string AdminChatId = "AdminChatId";
        
        private readonly string adminUserName;
        private readonly long adminChatId;

        public BotController(
            IConfiguration configuration, 
            IFeedbackRepository feedbackRepository, 
            ClientStatesService clientStates,
            IQuestionRepository questionRepository,
            INotificationService notificationService)
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

            _feedbackRepository = feedbackRepository;
            _questionRepository = questionRepository;
            _clientStatesService = clientStates;
            _notificationService = notificationService;
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
                    case UserState.FourthPresentationQuestion:
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
            var feedback = new FeedbackDbEntity
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
                await _feedbackRepository.CreateAsync(feedback).ConfigureAwait(false);
                await _notificationService.SendFeedbackAsync(feedback).ConfigureAwait(false);
                await client.SendTextMessageAsync(message.Chat.Id, $"Спасибо, ваш фидбэк '{feedback.GeneralFeedback}' и предложения '{feedback.FutureProposal}' были сохранены под ником '{feedback.AuthorName}'", replyMarkup: GetButtons()).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                await client.SendTextMessageAsync(adminChatId, $"Exception: {ex.Message}").ConfigureAwait(false);
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
                    UserState.FirstPresentationQuestion => new Guid("db121e8c-9a90-4d58-cb26-08da193993e3"),
                    UserState.SecondPresentationQuestion => new Guid("0c90d5f2-7894-4446-cb27-08da193993e3"),
                    UserState.ThirdPresentationQuestion => new Guid("ca63dc5f-4086-488e-cb28-08da193993e3"),
                    UserState.FourthPresentationQuestion => new Guid("4dcac281-f7cd-4593-cb29-08da193993e3"),
                    UserState.OutOfPresentationQuestion => new Guid("958AE825-56F4-4390-90E3-4AA9741673A3"),
                    UserState.LeaveFeedback => throw new Exception($"Invalid user state = {clientService.UserState}"),
                    _ => throw new Exception($"Invalid user state = {clientService.UserState}")
                };
                var question = new QuestionDbEntity
                {
                    Id = Guid.NewGuid(),
                    Date = DateTime.Now.Date,
                    Text = clientService.QuestionText,
                    AuthorName = AuthorNameGenerator.Generate(),
                    PresentationId = presentationId,
                    Time = DateTime.Now.TimeOfDay
                };
                await _questionRepository.CreateAsync(question).ConfigureAwait(false);
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
                case MainKeyboard.FourthPresentationQuestion:
                    _clientStatesService.SetUserState(message.Chat.Id, UserState.FourthPresentationQuestion);
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
                    new List<KeyboardButton>
                    {
                        new KeyboardButton(text: MainKeyboard.FirstPresentationQuestion),
                        new KeyboardButton(text: MainKeyboard.SecondPresentationQuestion)
                    },
                    new List<KeyboardButton>
                    {
                        new KeyboardButton(text: MainKeyboard.ThirdPresentationQuestion),
                        new KeyboardButton(text: MainKeyboard.FourthPresentationQuestion),
                    },
                    new List<KeyboardButton>
                    {
                        new KeyboardButton(text: MainKeyboard.OutOfPresentationQuestion),
                        new KeyboardButton(text: MainKeyboard.LeaveFeedback),
                    }
                }
             );
        }
        #endregion
    }
}
