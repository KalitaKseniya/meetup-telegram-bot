using MeetupTelegramBot.DataAccess.Contexts;
using MeetupTelegramBot.DataAccess.Entities;
using MeetupTelegramBot.DataAccess.Interfaces;

namespace MeetupTelegramBot.DataAccess.Repositories
{
    public class FeedbackRepository : IFeedbackRepository
    {
        private readonly DatabaseContext _databaseContext;

        public FeedbackRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public Task CreateAsync(FeedbackEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<List<FeedbackEntity>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<FeedbackEntity> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
