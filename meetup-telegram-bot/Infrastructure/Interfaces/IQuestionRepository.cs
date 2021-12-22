using meetup_telegram_bot.Data.DbEntities;

namespace meetup_telegram_bot.Infrastructure.Interfaces
{
    public interface IQuestionRepository
    {
        Task CreateAsync(QuestionDbEntity dbEntity);
        Task<List<QuestionDbEntity>> GetAllAsync();
        Task<List<QuestionDbEntity>> GetByPresentationIdAsync(Guid? presentationId);
    }
}
