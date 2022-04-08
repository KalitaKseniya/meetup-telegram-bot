using meetup_telegram_bot.Controllers.Boundary.Request;
using meetup_telegram_bot.Controllers.Boundary.Response;
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
            var presentationsFromDb = await _presentationRepository.GetDisplayedAsync();
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
      
        // ToDo : make endpoint to update isDisplayed
        // ReDo static
        [HttpPost]
        public async Task<IActionResult> CreatePresentation([FromBody] PresentationForCreationDto presentationDto)
        {
            if (presentationDto == null)
            {
                return BadRequest("Model cannot be null");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var presentationDb = presentationDto.ToDbEntity();
            await _presentationRepository.CreateAsync(presentationDb);

            return new ObjectResult(presentationDb) { StatusCode = StatusCodes.Status201Created };
        }

        [HttpPut]
        public async Task<IActionResult> UpdateDisplayedPresentations([FromBody] List<PresentationForUpdateDto> displayedPresentationsNewIds)
        {
            // ToDo: handle existence if exist

            //
            await _presentationRepository.UpdateDisplayedPresentations(displayedPresentationsNewIds.Select(x => x.Id).ToList());
            return NoContent();
        }
    }
}
