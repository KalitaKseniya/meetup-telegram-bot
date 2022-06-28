using AutoMapper;
using meetup_telegram_bot.SignalR.Models;
using MeetupTelegramBot.BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace meetup_telegram_bot.Controllers
{
    [ApiController]
    [Route("api/questions")]
    public class QuestionsController
    {
        private readonly IQuestionService _questionService;
        private readonly IMapper _mapper;

        public QuestionsController(IQuestionService questionService,
            IMapper mapper)
        {
            _questionService = questionService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<List<QuestionModel>> GetQuestions()
        {
            var questionsDto = await _questionService.GetAllAsync();
            return _mapper.Map<List<QuestionModel>>(questionsDto);
        }
    }
}
