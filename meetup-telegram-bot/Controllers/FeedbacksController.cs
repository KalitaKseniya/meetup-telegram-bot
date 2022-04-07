using meetup_telegram_bot.SignalR.Models;
using MeetupTelegramBot.BusinessLayer.Factories;
using MeetupTelegramBot.BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace meetup_telegram_bot.Controllers
{
    [ApiController]
    [Route("api/feedbacks")]
    public class FeedbacksController
    {
        private readonly IFeedbackService _feedbackService;

        public FeedbacksController(IFeedbackService feedbackService)
        {
            _feedbackService = feedbackService;
        }

        [HttpGet]
        public async Task<List<FeedbackModel>> GetFeedbacks()
        {
            var feedbacksFromDb = await _feedbackService.GetAllAsync().ConfigureAwait(false);
            return feedbacksFromDb.ToModel();
        }
    }
}
