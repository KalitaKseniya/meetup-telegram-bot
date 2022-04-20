using MeetupTelegramBot.DataAccess.Contexts;
using MeetupTelegramBot.DataAccess.Entities;
using MeetupTelegramBot.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MeetupTelegramBot.DataAccess.Repositories
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly DatabaseContext _databaseContext;

        public QuestionRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task CreateAsync(QuestionEntity entity)
        {
            await _databaseContext.Questions.AddAsync(entity);
            await _databaseContext.SaveChangesAsync();
        }

        public async Task<List<QuestionEntity>> GetAllAsync()
        {
            return await _databaseContext.Questions
                .OrderByDescending(q => q.Date)
                .ThenByDescending(q => q.Time)
                .ToListAsync();
        }

        public async Task<List<QuestionEntity>> GetByPresentationIdAsync(Guid presentationId)
        {
            return await _databaseContext.Questions.Where(q => q.PresentationId == presentationId)
                .Where(q => q.Date == DateTime.Today.Date)
                .OrderByDescending(q => q.Time)
                
                .ToListAsync();
        }
    }
}
