using MeetupTelegramBot.DataAccess.Contexts;
using MeetupTelegramBot.DataAccess.Entities;
using MeetupTelegramBot.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetupTelegramBot.DataAccess.Repositories
{
    public class SpeackerRepository : RepositoryBase<SpeackerEntity>, ISpeackerRepository
    {
        public SpeackerRepository(DatabaseContext databaseContext)
            : base(databaseContext)
        {
        }

        public SpeackerEntity GetById(Guid speackerId)
        {
            return FindByCondition(s => s.Id == speackerId, false).FirstOrDefault();
        }
    }
}
