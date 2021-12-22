using meetup_telegram_bot.SignalR.Models;

namespace meetup_telegram_bot.SignalR.Interfaces
{
    public interface IChatHub
    {
        Task SendFeedback(FeedbackModel feedback);

        Task SendQuestion(QuestionModel question);
        Task SendQuestions(List<QuestionModel> questions);
        Task SendFeedbacks(List<FeedbackModel> feedbacks);
    }
}
