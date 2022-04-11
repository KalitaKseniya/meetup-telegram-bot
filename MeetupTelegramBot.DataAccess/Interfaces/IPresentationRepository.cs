using MeetupTelegramBot.DataAccess.Entities;
using MeetupTelegramBot.DataAccess.Repositories;

namespace MeetupTelegramBot.DataAccess.Interfaces
{
    public interface IPresentationRepository
    {
        Task CreateAsync(PresentationEntity entity);
        Task<List<PresentationEntity>> GetAllAsync();
        Task<List<PresentationEntity>> GetDisplayedAsync();
        Task UpdateDisplayedAsync(List<Guid> displayedPresentationsNewIds);
        bool PresentationExist(Guid id);
    }
}
