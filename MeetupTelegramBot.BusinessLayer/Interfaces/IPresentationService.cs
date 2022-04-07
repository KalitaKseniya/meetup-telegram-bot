using MeetupTelegramBot.BusinessLayer.Models.DTO;

namespace MeetupTelegramBot.BusinessLayer.Interfaces;

public interface IPresentationService
{
    Task CreateAsync(PresentationDTO entity);
    Task<List<PresentationDTO>> GetAllAsync();
    Task<List<PresentationDTO>> GetDisplayedAsync();
}