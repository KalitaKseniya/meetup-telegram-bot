using MeetupTelegramBot.BusinessLayer.Models.DTO;

namespace MeetupTelegramBot.BusinessLayer.Interfaces;

public interface IQuestionService
{
    Task CreateAsync(QuestionDTO entity);
    Task<List<QuestionDTO>> GetAllAsync();
    Task<List<QuestionDTO>> GetByPresentationIdAsync(Guid? presentationId);
}