using MeetupTelegramBot.DataAccess.Entities;

namespace MeetupTelegramBot.DataAccess.Interfaces
{
    public interface ISpeackerRepository
    {
        SpeackerEntity GetById(Guid speackerId);
    }
}
