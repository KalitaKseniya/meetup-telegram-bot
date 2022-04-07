using meetup_telegram_bot.SignalR.Models;
using MeetupTelegramBot.BusinessLayer.Factories;
using MeetupTelegramBot.BusinessLayer.Interfaces;
using MeetupTelegramBot.BusinessLayer.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace meetup_telegram_bot.Controllers
{
    [ApiController]
    [Route("api/presentations")]
    public class PresentationsController : ControllerBase
    {
        private readonly IQuestionService _questionService;
        private readonly IPresentationService _presentationService;

        public PresentationsController(IQuestionService questionService, IPresentationService presentationService)
        {
            _questionService = questionService;
            _presentationService=presentationService;
        }

        /// <summary>
        /// Endpoint to get questions for a speceific presentations (by presentation id) or to get questions out of presentation (by default word)
        /// </summary>
        /// <param name="presentationId">Id of presentation (Guid) or "default" for questions out of presentation</param>
        /// <returns></returns>
        [HttpGet("{presentationId}/questions")]
        public async Task<List<QuestionModel>> GetQuestionsByPresentationId(string presentationId)
        {
            Guid presentationIdFromRoute;
            var questionsForPresentation = Guid.TryParse(presentationId, out presentationIdFromRoute);

            var questionsFromDb = questionsForPresentation? await _questionService.GetByPresentationIdAsync(presentationIdFromRoute).ConfigureAwait(false) :
                                                        new List<QuestionDTO>();


            return questionsFromDb.ToModel();
        }

        /// <summary>
        /// Returns a list of all displayed presentations
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<PresentationModel>> GetDisplayedPresentations()
        {
            var presentationsFromDb = await _presentationService.GetDisplayedAsync().ConfigureAwait(false);
            return presentationsFromDb.ToModel();
        }
        
        /// <summary>
        /// Returns a list of all presentations
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("all")]
        public async Task<List<PresentationModel>> GetPresentations()
        {
            var presentationsFromDb = await _presentationService.GetAllAsync().ConfigureAwait(false);
            return presentationsFromDb.ToModel();
        }
    }
}
