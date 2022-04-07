using MeetupTelegramBot.DataAccess.Contexts;
using MeetupTelegramBot.DataAccess.Entities;
using MeetupTelegramBot.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MeetupTelegramBot.DataAccess.Repositories
{
    public class PresentationRepository : IPresentationRepository
    {
        private readonly DatabaseContext _databaseContext;

        public PresentationRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public Task CreateAsync(PresentationEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<List<PresentationEntity>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<PresentationEntity>> GetDisplayedAsync()
        {
            throw new NotImplementedException();
        }
    }
}
