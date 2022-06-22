using meetup_telegram_bot.SignalR.Models;
using MeetupTelegramBot.BusinessLayer.Factories;
using MeetupTelegramBot.BusinessLayer.Interfaces;
using MeetupTelegramBot.BusinessLayer.Models.DTO;
using MeetupTelegramBot.BusinessLayer.Models.DTO.Request;
using MeetupTelegramBot.BusinessLayer.Services;
using MeetupTelegramBot.DataAccess.Contexts;
using Microsoft.AspNetCore.Mvc;

namespace meetup_telegram_bot.Controllers
{
    [ApiController]
    [Route("api/presentations")]
    public class PresentationsController : ControllerBase
    {
        private readonly IQuestionService _questionService;
        private readonly IPresentationService _presentationService;
        private readonly ClientStatesService _clientStatesService;


        public PresentationsController(IQuestionService questionService, ClientStatesService clientStates, IPresentationService presentationService)
        {
            _questionService = questionService;
            _presentationService = presentationService;
            _clientStatesService = clientStates;
        }

        /// <summary>
        /// Endpoint to get questions for a speceific presentations (by presentation id) 
        /// </summary>
        /// <param name="presentationId">Id of presentation (Guid) </param>
        /// <returns></returns>
        [HttpGet("{presentationId}/questions")]
        public async Task<List<QuestionModel>> GetQuestionsByPresentationId(Guid presentationId)
        {
            var questionsDto = await _questionService.GetByPresentationIdAsync(presentationId);

            return questionsDto.ToModel();
        }

        /// <summary>
        /// Returns a list of all displayed presentations
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<PresentationModel>> GetDisplayedPresentations()
        {
            var displayedPresentations = await _presentationService.GetDisplayedAsync();
            return displayedPresentations.ToModel();
        }

        /// <summary>
        /// Returns a list of all presentations
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("all")]
        public async Task<  List<PresentationModel>> GetPresentations()
        {
            var presentations = await _presentationService.GetAllAsync();
            return presentations.ToModel();
        }
      
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
            var createdPresentation = await _presentationService.CreateAsync(presentationDto);

            return new ObjectResult(createdPresentation) { StatusCode = StatusCodes.Status201Created };
        }

        [HttpPut]
        public async Task<IActionResult> UpdateDisplayedPresentations([FromBody] List<PresentationForUpdateDto> presentationsToUpdate)
        {
            await _presentationService.UpdateDisplayedAsync(presentationsToUpdate);
            await _clientStatesService.SetPresentationsAsync();
            return NoContent();
        }
    }
}
