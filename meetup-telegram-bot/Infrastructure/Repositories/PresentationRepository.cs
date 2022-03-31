using meetup_telegram_bot.Data.DbEntities;
using meetup_telegram_bot.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace meetup_telegram_bot.Infrastructure.Repositories
{
    public class PresentationRepository : IPresentationRepository
    {
        private readonly DatabaseContext _databaseContext;

        public PresentationRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }
        public async Task CreateAsync(PresentationDbEntity dbEntity)
        {
            await _databaseContext.Presentations.AddAsync(dbEntity).ConfigureAwait(false);
            await _databaseContext.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task<List<PresentationDbEntity>> GetAllAsync()
        {
            return await _databaseContext.Presentations
                .ToListAsync()
                .ConfigureAwait(false);
        }
        
        public async Task<List<PresentationDbEntity>> GetDisplayedAsync()
        {
            return await _databaseContext.Presentations
                .Where(x => x.IsDisplayed == true)
                .ToListAsync()
                .ConfigureAwait(false);
        }
    }
}
