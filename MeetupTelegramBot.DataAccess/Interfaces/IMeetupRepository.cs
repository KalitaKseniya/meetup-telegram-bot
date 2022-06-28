using MeetupTelegramBot.DataAccess.Entities;

namespace MeetupTelegramBot.DataAccess.Interfaces
{
    public interface IMeetupRepository
    {
        bool Exists(Guid id);
        Task<List<MeetupEntity>> GetAllAsync();
    }
}
