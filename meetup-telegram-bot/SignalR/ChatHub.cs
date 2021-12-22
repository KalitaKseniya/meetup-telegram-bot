using meetup_telegram_bot.SignalR.Interfaces;
using meetup_telegram_bot.SignalR.Models;
using Microsoft.AspNetCore.SignalR;

namespace meetup_telegram_bot.SignalR
{
    public class ChatHub : Hub<IChatHub>
    {
        public async Task SendFeedback(FeedbackModel feedback)
        {
            await Clients.All.SendFeedback(feedback).ConfigureAwait(false);
        }
        public async Task SendQuestion(QuestionModel question)
        {
            await Clients.All.SendQuestion(question).ConfigureAwait(false);
        }
    }
}
