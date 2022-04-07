
using MeetupTelegramBot.DataAccess.Entities;

namespace MeetupTelegramBot.DataAccess.Interfaces
{
    public interface IFeedbackRepository
    {
        Task CreateAsync(FeedbackEntity entity);
        Task<List<FeedbackEntity>> GetAllAsync();
        Task<FeedbackEntity> GetByIdAsync(Guid id);
    }
}
