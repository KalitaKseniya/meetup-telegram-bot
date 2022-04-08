using meetup_telegram_bot.Data.DbEntities;

namespace meetup_telegram_bot.Infrastructure.Interfaces
{
    public interface IPresentationRepository
    {
        Task CreateAsync(PresentationDbEntity dbEntity);
        Task<List<PresentationDbEntity>> GetAllAsync();
        Task<List<PresentationDbEntity>> GetDisplayedAsync();
        Task UpdateDisplayedPresentations(List<Guid> displayedPresentationsNewIds);
        
    }
}
