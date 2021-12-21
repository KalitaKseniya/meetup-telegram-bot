using meetup_telegram_bot.Data.DbEntities;
using meetup_telegram_bot.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace meetup_telegram_bot.Controllers
{
    [ApiController]
    [Route("api/bot")]
    public class BotController : ControllerBase
    {
        private readonly TelegramBotClient client; 
        private readonly IFeedbackRepository _feedbackRepository;

        private const string TelegramBotToken = "TelegramBotToken";
        private const string AdminUserName = "AdminUserName";
        private const string AdminChatId = "AdminChatId";
        
        private readonly string adminUserName;
        private readonly long adminChatId;

        public BotController(IConfiguration configuration, IFeedbackRepository feedbackRepository)
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
        }
        
        [HttpPost]
        public async Task Post([FromBody] Update update)
        {
            if (update == null)
            {
                throw new ArgumentNullException(nameof(update));
            }
            var message = update.Message;
            if (message.Chat.Id != adminChatId)
            {
                await client.SendTextMessageAsync(message.Chat.Id, $"Бот находится в стадии разработки, по вопросам пишите, пожалуйста: {adminUserName}.").ConfigureAwait(false);
                return;
            }
            
            if (message?.Type == MessageType.Text)
            {
                var feedback = new FeedbackDbEntity
                {
                    Id = Guid.NewGuid(),
                    Date = DateTime.Now.Date,
                    FutureProposal = "There will be future proposal.",
                    GeneralFeedback = message.Text,
                    Time = DateTime.Now.TimeOfDay
                };
                try
                {
                    await _feedbackRepository.CreateAsync(feedback).ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    await client.SendTextMessageAsync(message.Chat.Id, $"Exception: {ex.Message}").ConfigureAwait(false);
                }
                await client.SendTextMessageAsync(message.Chat.Id, $"Спасибо, ваш фидбэк был сохранен: {message.Text}").ConfigureAwait(false);
            }
        }
        
        [HttpGet]
        public string Get(string msg)
        {
           return msg;
        }
    }
}
