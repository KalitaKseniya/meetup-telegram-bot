using MeetupTelegramBot.DataAccess.Contexts;
using MeetupTelegramBot.DataAccess.Entities;
using MeetupTelegramBot.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MeetupTelegramBot.DataAccess.Repositories
{
    public class FeedbackRepository : IFeedbackRepository
    {
        private readonly DatabaseContext _databaseContext;

        public FeedbackRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task CreateAsync(FeedbackEntity entity)
        {
            await _databaseContext.Feedbacks.AddAsync(entity);

            _databaseContext.SaveChanges();
        }

        public async Task<List<FeedbackEntity>> GetAllAsync()
        {
            return await _databaseContext.Feedbacks
                .OrderByDescending(p => p.Time)
                .ToListAsync();
        }

        public async Task<FeedbackEntity> GetByIdAsync(Guid id)
        {
            return await _databaseContext
                .Feedbacks
                .Where(f => f.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<List<FeedbackEntity>> GetByMeetupIdAsync(Guid meetupId)
        {
            return await _databaseContext
             .Feedbacks
             .Where(f => f.MeetupId == meetupId)
             .ToListAsync();
        }
    }
}
