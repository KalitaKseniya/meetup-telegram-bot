
using MeetupTelegramBot.DataAccess.Entities;

namespace MeetupTelegramBot.DataAccess.Interfaces
{
    public interface IQuestionRepository
    {
        Task CreateAsync(QuestionEntity entity);
        Task<List<QuestionEntity>> GetAllAsync();
        Task<List<QuestionEntity>> GetByPresentationIdAsync(Guid? presentationId);
    }
}
