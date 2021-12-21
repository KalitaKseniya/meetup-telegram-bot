using meetup_telegram_bot.Data;
using meetup_telegram_bot.Data.DbEntities;
using meetup_telegram_bot.Infrastructure;
using meetup_telegram_bot.Infrastructure.Interfaces;
using meetup_telegram_bot.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
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
        private readonly IHubContext<ChatHub, IChatHub> _hubContext;

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
            IHubContext<ChatHub, IChatHub> hubContext)
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
            _hubContext = hubContext;
        }
        
        [HttpPost]
        public async Task Post([FromBody] Update update)
        {
            if (update == null)
            {
                throw new ArgumentNullException(nameof(update));
            }
            
            await ProcessUpdate(update);
        }
        
        [HttpGet]
        public async Task<string> Get(string msg)
        {
            await _hubContext.Clients.All.Send(msg).ConfigureAwait(false);

            return msg;
        }

        private async Task ProcessUpdate(Update update)
        {
            switch (update.Type)
            {
                case UpdateType.Message:
                    var message = update.Message;
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
                                if (_clientStatesService.ClientStates[message.Chat.Id].QuestionText == null)
                                {
                                    await ProcessPresentationQuestion(message).ConfigureAwait(false);
                                }
                                else
                                {
                                    await ProcessPresentationAuthorName(message).ConfigureAwait(false);
                                }
                                break;
                            default:
                                //ToDo: implement 
                                await client.SendTextMessageAsync(message.Chat.Id, $"Sorry a haven't been implemented yet.").ConfigureAwait(false);
                                break;
                        }
                    }
                    else 
                    {
                        await ProcessMainKeyboard(message).ConfigureAwait(false);
                    }
                    
                    break;
                default:
                    throw new NotImplementedException();
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
                Time = DateTime.Now.TimeOfDay
            };
            try
            {
                await _feedbackRepository.CreateAsync(feedback).ConfigureAwait(false);
                await client.SendTextMessageAsync(message.Chat.Id, $"Спасибо, ваш фидбэк {feedback.GeneralFeedback} и предложения {feedback.FutureProposal} были сохранены", replyMarkup: GetButtons()).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                await client.SendTextMessageAsync(message.Chat.Id, $"Exception: {ex.Message}").ConfigureAwait(false);
            }
            _clientStatesService.ClientStates.Remove(message.Chat.Id);
        }
        
        private async Task ProcessPresentationAuthorName(Message message)
        {
            var clientService = _clientStatesService.ClientStates[message.Chat.Id];
            Guid? presentationId = clientService.UserState switch
            {
                UserState.FirstPresentationQuestion => new Guid("f7cd069c-b314-45e3-9589-7796e45e5e01"),
                UserState.SecondPresentationQuestion => new Guid("dacb7cdf-ad5a-4cd1-83d4-a02678fd1313"),
                UserState.ThirdPresentationQuestion => new Guid("3a8bc096-dff2-4e31-b45a-010a47322836"),
                UserState.OutOfPresentationQuestion => null,
                UserState.LeaveFeedback => throw new NotImplementedException(),
                _ => throw new Exception("Invalid user state")
            };
            
            var question = new QuestionDbEntity
            {
                Id = Guid.NewGuid(),
                Date = DateTime.Now.Date,
                Text = clientService.QuestionText,
                AuthorName = message.Text,
                PresentationId = presentationId,
                Time = DateTime.Now.TimeOfDay
            };
            try
            {
                await _questionRepository.CreateAsync(question).ConfigureAwait(false);
                await client.SendTextMessageAsync(message.Chat.Id, $"Спасибо, ваш вопрос {question.Text} и никнейм {question.AuthorName} были сохранены", replyMarkup: GetButtons()).ConfigureAwait(false);
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
            await client.SendTextMessageAsync(message.Chat.Id, "Напишите предложения для будущих митапов!", replyMarkup: null).ConfigureAwait(false);
        }
        
        private async Task ProcessPresentationQuestion(Message message)
        {
            _clientStatesService.SetPresentationQuestion(message.Chat.Id, message.Text);
            await client.SendTextMessageAsync(message.Chat.Id, "Введите никнейм для ответа!", replyMarkup: null).ConfigureAwait(false);
        }

        //ToDo: add back to main menu button
        private async Task ProcessMainKeyboard(Message message)
        {
            const string text1 = "Пожалуйста, введите свой вопрос";
            const string text2 = "Оставьте свой фидбэк";

            switch (message.Text)
            {
                case MainKeyboard.FirstPresentationQuestion:
                    _clientStatesService.SetUserState(message.Chat.Id, UserState.FirstPresentationQuestion);
                    await client.SendTextMessageAsync(message.Chat.Id, text1, replyMarkup: new ReplyKeyboardRemove()).ConfigureAwait(false);
                    break;
                case MainKeyboard.SecondPresentationQuestion:
                    _clientStatesService.SetUserState(message.Chat.Id, UserState.SecondPresentationQuestion);
                    await client.SendTextMessageAsync(message.Chat.Id, text1, replyMarkup: new ReplyKeyboardRemove()).ConfigureAwait(false);
                    break;
                case MainKeyboard.ThirdPresentationQuestion:
                    _clientStatesService.SetUserState(message.Chat.Id, UserState.ThirdPresentationQuestion);
                    await client.SendTextMessageAsync(message.Chat.Id, text1, replyMarkup: new ReplyKeyboardRemove()).ConfigureAwait(false);
                    break;
                case MainKeyboard.OutOfPresentationQuestion:
                    _clientStatesService.SetUserState(message.Chat.Id, UserState.OutOfPresentationQuestion);
                    await client.SendTextMessageAsync(message.Chat.Id, text1, replyMarkup: new ReplyKeyboardRemove()).ConfigureAwait(false);
                    break;
                case MainKeyboard.LeaveFeedback:
                    _clientStatesService.SetUserState(message.Chat.Id, UserState.LeaveFeedback);
                    await client.SendTextMessageAsync(message.Chat.Id, text2, replyMarkup: new ReplyKeyboardRemove()).ConfigureAwait(false);
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
                        new KeyboardButton(text: MainKeyboard.OutOfPresentationQuestion)
                    },
                    new List<KeyboardButton>
                    {
                        new KeyboardButton(text: MainKeyboard.LeaveFeedback),
                    }
                }
             );
        }
    }
}
