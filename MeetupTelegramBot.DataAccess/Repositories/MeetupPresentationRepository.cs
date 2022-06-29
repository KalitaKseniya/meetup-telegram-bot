using MeetupTelegramBot.DataAccess.Contexts;
using MeetupTelegramBot.DataAccess.Entities;
using MeetupTelegramBot.DataAccess.Interfaces;

namespace MeetupTelegramBot.DataAccess.Repositories
{
    public class MeetupPresentationRepository : RepositoryBase<MeetupPresentationEntity>, IMeetupPresentationRepository
    {
        public MeetupPresentationRepository(DatabaseContext databaseContext)
            : base(databaseContext)
        {
        }

        public async Task CreateAsync(MeetupPresentationEntity entity)
        {
            Create(entity);

            await _databaseContext.SaveChangesAsync();
        }

        public MeetupPresentationEntity GetByMeetupAndPresentation(Guid meetupId, Guid presentationId)
        {
            return base.FindAll(false)
                .FirstOrDefault(mp => mp.MeetupId == meetupId && mp.PresentationId == presentationId);
        }
    }
}
