﻿using meetup_telegram_bot.Data.DbEntities;
using meetup_telegram_bot.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

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

        public async Task<List<QuestionDbEntity>> GetAllAsync()
        {
            return await _databaseContext.Questions
                .OrderByDescending(q => q.Date)
                .ThenByDescending(q => q.Time)
                .ToListAsync();
        }
        
        public async Task<List<QuestionDbEntity>> GetByPresentationIdAsync(Guid? presentationId)
        {
            return await _databaseContext.Questions.Where(q => q.PresentationId == presentationId)
                .OrderByDescending(q => q.Date)
                .ThenByDescending(q => q.Time)
                .ToListAsync();
        }
        
        public async Task<List<QuestionDbEntity>> GetOutOfPresentationAsync()
        {
            return await _databaseContext.Questions.Where(q => q.PresentationId == null)
                .OrderByDescending(q => q.Date)
                .ThenByDescending(q => q.Time)
                .ToListAsync();
        }
    }
}