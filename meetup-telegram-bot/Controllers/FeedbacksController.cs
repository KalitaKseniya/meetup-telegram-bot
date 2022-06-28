using AutoMapper;
using meetup_telegram_bot.SignalR.Models;
using MeetupTelegramBot.BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace meetup_telegram_bot.Controllers
{
    [ApiController]
    [Route("api/feedbacks")]
    public class FeedbacksController
    {
        private readonly IFeedbackService _feedbackService;
        private readonly IMapper _mapper;

        public FeedbacksController(IFeedbackService feedbackService,
            IMapper mapper
            )
        {
            _feedbackService = feedbackService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<List<FeedbackModel>> GetFeedbacks()
        {
            var feedbacksDto = await _feedbackService.GetAllAsync();
            return _mapper.Map<List<FeedbackModel>>(feedbacksDto);
        }
    }
}
