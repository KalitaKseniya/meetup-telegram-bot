using meetup_telegram_bot.Data.DbEntities;
using meetup_telegram_bot.Factories;
using meetup_telegram_bot.Infrastructure.Interfaces;
using meetup_telegram_bot.SignalR.Models;
using Microsoft.AspNetCore.Mvc;

namespace meetup_telegram_bot.Controllers
{
    [ApiController]
    [Route("api/presentations")]
    public class PresentationsController : ControllerBase
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IPresentationRepository _presentationRepository;

        public PresentationsController(IQuestionRepository questionRepository, IPresentationRepository presentationRepository)
        {
            _questionRepository=questionRepository;
            _presentationRepository=presentationRepository;
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

            var questionsFromDb = questionsForPresentation? await _questionRepository.GetByPresentationIdAsync(presentationIdFromRoute).ConfigureAwait(false) :
                                                        new List<QuestionDbEntity>();


            return questionsFromDb.ToModel();
        }

        /// <summary>
        /// Returns a list of all displayed presentations
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<PresentationModel>> GetDisplayedPresentations()
        {
            var presentationsFromDb = await _presentationRepository.GetDisplayedAsync().ConfigureAwait(false);
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
            var presentationsFromDb = await _presentationRepository.GetAllAsync().ConfigureAwait(false);
            return presentationsFromDb.ToModel();
        }
    }
}
