using MeetupTelegramBot.BusinessLayer.Models.DTO;

namespace MeetupTelegramBot.BusinessLayer.Interfaces;

public interface IQuestionService
{
    Task CreateAsync(QuestionDTO entity);
    Task<List<QuestionDTO>> GetAllAsync();
    Task<List<QuestionDTO>> GetByMeetupPresentationIdAsync(Guid meetupId, Guid presentationId);
}