using MeetupTelegramBot.DataAccess.Contexts;
using MeetupTelegramBot.DataAccess.Entities;
using MeetupTelegramBot.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MeetupTelegramBot.DataAccess.Repositories
{
    public class MeetupRepository : RepositoryBase<MeetupEntity>, IMeetupRepository
    {
        public MeetupRepository(DatabaseContext databaseContext)
            : base(databaseContext)
        {
        }

        public async Task<List<MeetupEntity>> GetAllAsync()
        {
            return await base.FindAll(false)
                .ToListAsync();
        }

        public bool Exists(Guid id)
        {
            return base.FindAll(false).Any(m => m.Id == id);
        }
    }
}
