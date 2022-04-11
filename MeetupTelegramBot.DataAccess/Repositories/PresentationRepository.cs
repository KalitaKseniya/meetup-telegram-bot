using MeetupTelegramBot.DataAccess.Contexts;
using MeetupTelegramBot.DataAccess.Entities;
using MeetupTelegramBot.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MeetupTelegramBot.DataAccess.Repositories
{
    public class PresentationRepository : RepositoryBase<PresentationEntity>, IPresentationRepository
    {
        private readonly DatabaseContext _databaseContext;

        public PresentationRepository(DatabaseContext databaseContext)
            : base(databaseContext)
        {
        }

        public async Task CreateAsync(PresentationEntity dbEntity)
        {
            await _databaseContext.Presentations.AddAsync(dbEntity);
            await _databaseContext.SaveChangesAsync();
        }

        public async Task<List<PresentationEntity>> GetAllAsync()
        {
            return await _databaseContext.Presentations
                .ToListAsync();
        }

        public async Task<List<PresentationEntity>> GetDisplayedAsync()
        {
            return await _databaseContext.Presentations
                .Where(x => x.IsDisplayed == true)
                .ToListAsync();
        }

        public bool PresentationExist(Guid id) 
        { 
            return _databaseContext.Presentations.Any(p => p.Id == id);
        }

        public IQueryable<PresentationEntity> FindByCondition(Expression<Func<PresentationEntity, bool>> expression)
        {
            return _databaseContext.Presentations.Where(expression);
        }

        public async Task SaveChagesAsync()
        {
            await _databaseContext.SaveChangesAsync();
        }
    }
}
