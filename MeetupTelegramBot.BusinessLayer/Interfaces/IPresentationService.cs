using MeetupTelegramBot.BusinessLayer.Models.DTO;
using MeetupTelegramBot.BusinessLayer.Models.DTO.Request;

namespace MeetupTelegramBot.BusinessLayer.Interfaces;

public interface IPresentationService
{
    Task<PresentationDTO> CreateAsync(PresentationForCreationDto entity);
    Task<List<PresentationDTO>> GetAllAsync();
}