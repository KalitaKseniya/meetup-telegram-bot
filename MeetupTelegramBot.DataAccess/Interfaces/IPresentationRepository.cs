
using MeetupTelegramBot.DataAccess.Entities;

namespace MeetupTelegramBot.DataAccess.Interfaces
{
    public interface IPresentationRepository
    {
        Task CreateAsync(PresentationEntity entity);
        Task<List<PresentationEntity>> GetAllAsync();
        Task<List<PresentationEntity>> GetDisplayedAsync();
    }
}
