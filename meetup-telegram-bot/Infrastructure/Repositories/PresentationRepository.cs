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

        public async Task UpdateDisplayedPresentations(List<Guid> displayedPresentationsNewIds)
        {
            var oldPresentatnions = _databaseContext.Presentations.Where(p => p.IsDisplayed == true && p.Title != "");
            await oldPresentatnions.ForEachAsync(x => x.IsDisplayed = false);

            var newPresentations = _databaseContext.Presentations.Where(p => displayedPresentationsNewIds.Contains(p.Id));
            
            await newPresentations.ForEachAsync(x => x.IsDisplayed = true);
            await _databaseContext.SaveChangesAsync();
        }
    }
}
