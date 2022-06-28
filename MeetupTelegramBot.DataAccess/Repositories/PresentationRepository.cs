using MeetupTelegramBot.DataAccess.Contexts;
using MeetupTelegramBot.DataAccess.Entities;
using MeetupTelegramBot.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace MeetupTelegramBot.DataAccess.Repositories
{
    public class PresentationRepository : RepositoryBase<PresentationEntity>, IPresentationRepository
    {
        public PresentationRepository(DatabaseContext databaseContext)
            : base(databaseContext)
        {
        }

        public void Create(PresentationEntity dbEntity)
        {
            base.Create(dbEntity);
        }

        public async Task<List<PresentationEntity>> GetAllAsync()
        {
            return await base.FindAll(false)
                .Include(s => s.Speacker)
                .ToListAsync();
        }

        public bool PresentationExist(Guid id) 
        { 
            return base.FindAll(false).Any(p => p.Id == id);
        }

        public IQueryable<PresentationEntity> FindByCondition(Expression<Func<PresentationEntity, bool>> expression, bool trackChanges)
        {
            return base.FindByCondition(expression, trackChanges);
        }

        public bool NotContainsAll(List<Guid> ids)
        {
            return ids.Any(id => !base.FindAll(false)
                                    .Select(p => p.Id)
                                    .Contains(id));
        }

        public async Task SaveChangesAsync()
        {
            await base.SaveChangesAsync();
        }
    }
}
