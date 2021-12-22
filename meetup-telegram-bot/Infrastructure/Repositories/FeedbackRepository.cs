using meetup_telegram_bot.Data.DbEntities;
using meetup_telegram_bot.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace meetup_telegram_bot.Infrastructure.Repositories
{
    public class FeedbackRepository : IFeedbackRepository
    {
        private readonly DatabaseContext _databaseContext;

        public FeedbackRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task CreateAsync(FeedbackDbEntity dbEntity)
        {
            await _databaseContext.Feedbacks.AddAsync(dbEntity);
            await _databaseContext.SaveChangesAsync();
        }

        public async Task<List<FeedbackDbEntity>> GetAllAsync()
        {
            return await _databaseContext.Feedbacks
                .OrderByDescending(q => q.Date)
                .ThenByDescending(q => q.Time)
                .ToListAsync();
        }

        public async Task<FeedbackDbEntity> GetByIdAsync(Guid id)
        {
            return await _databaseContext.Feedbacks.FirstOrDefaultAsync(f => f.Id == id);
        }
    }
}
