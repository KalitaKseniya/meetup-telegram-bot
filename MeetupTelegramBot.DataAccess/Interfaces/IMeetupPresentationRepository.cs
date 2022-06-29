
using MeetupTelegramBot.DataAccess.Entities;

namespace MeetupTelegramBot.DataAccess.Interfaces
{
    public interface IMeetupPresentationRepository
    {
        Task CreateAsync(MeetupPresentationEntity entity);
        MeetupPresentationEntity GetByMeetupAndPresentation(Guid meetupId, Guid presentationId);
    }
}
