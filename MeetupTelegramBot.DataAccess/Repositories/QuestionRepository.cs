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

        public Task CreateAsync(QuestionEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<List<QuestionEntity>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<QuestionEntity>> GetByPresentationIdAsync(Guid? presentationId)
        {
            throw new NotImplementedException();
        }
    }
}
