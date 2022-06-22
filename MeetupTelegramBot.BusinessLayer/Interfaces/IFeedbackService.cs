using MeetupTelegramBot.BusinessLayer.Models.DTO;

namespace MeetupTelegramBot.BusinessLayer.Interfaces;

public interface IFeedbackService
{
    Task CreateAsync(FeedbackDTO entity);
    Task<List<FeedbackDTO>> GetAllAsync();
    Task<FeedbackDTO> GetByIdAsync(Guid id);
}