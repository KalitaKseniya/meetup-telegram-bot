using meetup_telegram_bot.Data.DbEntities;

namespace meetup_telegram_bot.Infrastructure.Interfaces
{
    public interface IFeedbackRepository
    {
        Task CreateAsync(FeedbackDbEntity dbEntity);
        Task<List<FeedbackDbEntity>> GetAllAsync();
        Task<FeedbackDbEntity> GetByIdAsync(Guid id);
    }
}
