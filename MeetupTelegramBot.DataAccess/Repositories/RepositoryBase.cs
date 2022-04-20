using MeetupTelegramBot.DataAccess.Contexts;
using MeetupTelegramBot.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MeetupTelegramBot.DataAccess.Repositories
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected DatabaseContext _databaseContext;
        public RepositoryBase(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public IQueryable<T> FindAll(bool trackChanges)
        {
            return !trackChanges ? 
                _databaseContext.Set<T>()
                    .AsNoTracking() :
                _databaseContext.Set<T>();
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges)
        {
             return !trackChanges ?
                _databaseContext.Set<T>()
                    .Where(expression)
                    .AsNoTracking() :
                _databaseContext.Set<T>()
                    .Where(expression);
        }

        public void Create(T entity) => _databaseContext.Set<T>().Add(entity);
        public void Update(T entity) => _databaseContext.Set<T>().Update(entity);
        public void Delete(T entity) => _databaseContext.Set<T>().Remove(entity);

        public async Task SaveChangesAsync() => await _databaseContext.SaveChangesAsync();
    }
}
