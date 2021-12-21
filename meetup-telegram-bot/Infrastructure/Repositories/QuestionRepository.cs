using meetup_telegram_bot.Data.DbEntities;
using meetup_telegram_bot.Infrastructure.Interfaces;

namespace meetup_telegram_bot.Infrastructure.Repositories
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly DatabaseContext _databaseContext;

        public QuestionRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }
        
        public async Task CreateAsync(QuestionDbEntity dbEntity)
        {
            await _databaseContext.Questions.AddAsync(dbEntity);
            await _databaseContext.SaveChangesAsync();
        }
    }
}
