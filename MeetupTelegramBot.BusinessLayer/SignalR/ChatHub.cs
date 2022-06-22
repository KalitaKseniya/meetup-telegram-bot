using meetup_telegram_bot.SignalR.Models;
using MeetupTelegramBot.BusinessLayer.SignalR.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace MeetupTelegramBot.BusinessLayer.SignalR
{
    public class ChatHub : Hub<IChatHub>
    {
        public async Task SendFeedback(FeedbackModel feedback)
        {
            await Clients.All.SendFeedback(feedback);
        }
        public async Task SendQuestion(QuestionModel question)
        {
            await Clients.All.SendQuestion(question);
        }
    }
}
