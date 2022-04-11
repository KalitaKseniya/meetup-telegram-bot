using meetup_telegram_bot.Controllers.Boundary.Request;
using meetup_telegram_bot.Controllers.Boundary.Response;
using meetup_telegram_bot.Data.DbEntities;
using meetup_telegram_bot.Factories;
using meetup_telegram_bot.Infrastructure.Interfaces;
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
            _presentationService = presentationService;
        }

        /// <summary>
        /// Endpoint to get questions for a speceific presentations (by presentation id) 
        /// </summary>
        /// <param name="presentationId">Id of presentation (Guid) </param>
        /// <returns></returns>
        [HttpGet("{presentationId}/questions")]
        public async Task<List<QuestionModel>> GetQuestionsByPresentationId(Guid presentationId)
        {
            var questionsFromDb = await _questionService.GetByPresentationIdAsync(presentationId);

            return questionsFromDb.ToModel();
        }

        /// <summary>
        /// Returns a list of all displayed presentations
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<PresentationModel>> GetDisplayedPresentations()
        {
            var presentationsFromDb = await _presentationService.GetDisplayedAsync();
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
