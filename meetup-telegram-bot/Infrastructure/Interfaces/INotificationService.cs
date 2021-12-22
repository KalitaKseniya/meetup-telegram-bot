using meetup_telegram_bot.Data.DbEntities;

namespace meetup_telegram_bot.Infrastructure.Interfaces
{
    public interface INotificationService
    {
        public Task SendFeedbackAsync(FeedbackDbEntity feedback);
        public Task SendQuestionAsync(QuestionDbEntity question);
    }
}
