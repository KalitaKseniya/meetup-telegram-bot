using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        private const string TelegramBotToken = "TelegramBotToken";

        public BotController(IConfiguration configuration)
        {
            var token = configuration.GetSection("environmentVariables").GetValue<string>(TelegramBotToken);
            if (string.IsNullOrEmpty(token))
            {
                throw new Exception("Not found telegram bot token in configuration");
            }
            client = new TelegramBotClient(token);
        }
        
        [HttpPost]
        public async void Post([FromBody] Update update)
        {
            if (update == null) return;
            var message = update.Message;
            if (message?.Type == MessageType.Text)
            {
                await client.SendTextMessageAsync(message.Chat.Id, message.Text);
            }
        }
        
        [HttpGet]
        public string Get(string msg)
        {
           return msg;
        }
    }
}
