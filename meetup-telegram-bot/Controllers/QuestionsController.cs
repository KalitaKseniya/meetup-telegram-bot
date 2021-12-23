using meetup_telegram_bot.Factories;
using meetup_telegram_bot.Infrastructure.Interfaces;
using meetup_telegram_bot.SignalR.Models;
using Microsoft.AspNetCore.Mvc;

namespace meetup_telegram_bot.Controllers
{
    [ApiController]
    [Route("api/questions")]
    public class QuestionsController
    {
        private readonly IQuestionRepository _questionRepository;

        public QuestionsController(IQuestionRepository questionRepository)
        {
            _questionRepository = questionRepository;
        }

        [HttpGet]
        public async Task<List<QuestionModel>> GetQuestions()
        {
            var questionsFromDb = await _questionRepository.GetAllAsync().ConfigureAwait(false);
            return questionsFromDb.ToModel();
        }
    }
}
