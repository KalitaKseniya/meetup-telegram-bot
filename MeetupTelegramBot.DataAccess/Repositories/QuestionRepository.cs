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
                .OrderByDescending(q => q.Time)
                .ToListAsync();
        }

        public async Task<List<QuestionEntity>> GetByMeetupPresentationAsync(Guid meetupId, Guid presentationId)
        {
            var questions = from q in _databaseContext.Questions
                            join mp in _databaseContext.MeetupPresentations on q.MeetupPresentationId equals mp.Id
                            where mp.MeetupId == meetupId && mp.PresentationId == presentationId
                            orderby q.Time descending
                            select q;

            return await questions.ToListAsync();
        }
    }
}
