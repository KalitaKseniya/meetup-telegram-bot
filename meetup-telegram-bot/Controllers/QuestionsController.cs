using meetup_telegram_bot.SignalR.Models;
using MeetupTelegramBot.BusinessLayer.Factories;
using MeetupTelegramBot.BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace meetup_telegram_bot.Controllers
{
    [ApiController]
    [Route("api/questions")]
    public class QuestionsController
    {
        private readonly IQuestionService _questionService;

        public QuestionsController(IQuestionService questionService)
        {
            _questionService = questionService;
        }

        [HttpGet]
        public async Task<List<QuestionModel>> GetQuestions()
        {
            var questionsFromDb = await _questionService.GetAllAsync();
            return questionsFromDb.ToModel();
        }
    }
}
