using meetup_telegram_bot.Factories;
using meetup_telegram_bot.Infrastructure.Interfaces;
using meetup_telegram_bot.SignalR.Models;
using Microsoft.AspNetCore.Mvc;

namespace meetup_telegram_bot.Controllers
{
    [ApiController]
    [Route("api/feedbacks")]
    public class FeerbacksController
    {
        private readonly IFeedbackRepository _feedbackRepository;

        public FeerbacksController(IFeedbackRepository feedbackRepository)
        {
            _feedbackRepository=feedbackRepository;
        }

        [HttpGet]
        public async Task<List<FeedbackModel>> GetFeedbacks()
        {
            var feedbacksFromDb = await _feedbackRepository.GetAllAsync().ConfigureAwait(false);
            return feedbacksFromDb.ToModel();
        }
    }
}
