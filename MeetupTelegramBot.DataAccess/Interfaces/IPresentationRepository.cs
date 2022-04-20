using MeetupTelegramBot.DataAccess.Entities;
using MeetupTelegramBot.DataAccess.Repositories;
using System.Linq.Expressions;

namespace MeetupTelegramBot.DataAccess.Interfaces
{
    public interface IPresentationRepository
    {
        void Create(PresentationEntity entity);
        Task<List<PresentationEntity>> GetAllAsync();
        Task<List<PresentationEntity>> GetDisplayedAsync();
        bool PresentationExist(Guid id);
        IQueryable<PresentationEntity> FindByCondition(Expression<Func<PresentationEntity, bool>> expression, bool trackChanges);
        bool ContainsAll(List<Guid> ids);
        Task SaveChangesAsync();
    }
}
