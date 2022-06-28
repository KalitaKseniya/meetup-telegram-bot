using AutoMapper;
using meetup_telegram_bot.SignalR.Models;
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
        private readonly IMapper _mapper;

        public PresentationsController(IQuestionService questionService, 
            ClientStatesService clientStates, 
            IPresentationService presentationService,
            IMapper mapper
            )
        {
            _questionService = questionService;
            _presentationService = presentationService;
            _clientStatesService = clientStates;
            _mapper = mapper;
        }

        /// <summary>
        /// Returns a list of all presentations
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<PresentationModel>> GetPresentations()
        {
            var presentations = await _presentationService.GetAllAsync();
            return _mapper.Map<List<PresentationModel>>(presentations);
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
    }
}
