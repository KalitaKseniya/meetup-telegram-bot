using meetup_telegram_bot.SignalR.Models;

namespace MeetupTelegramBot.BusinessLayer.SignalR.Interfaces
{
    public interface IChatHub
    {
        Task SendFeedback(FeedbackModel feedback);

        Task SendQuestion(QuestionModel question);
    }
}
