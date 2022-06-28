using AutoMapper;
using meetup_telegram_bot.SignalR.Models;
using MeetupTelegramBot.BusinessLayer.Interfaces;
using MeetupTelegramBot.BusinessLayer.SignalR.Models;
using Microsoft.AspNetCore.Mvc;

namespace meetup_telegram_bot.Controllers
{
    [ApiController]
    [Route("api/meetups")]
    public class MeetupsController : ControllerBase
    {
        private readonly IQuestionService _questionService;
        private readonly IPresentationService _presentationService;
        private readonly IFeedbackService _feedbackService;
        private readonly IMeetupService _meetupService;
        private readonly IMapper _mapper;
        
        public MeetupsController(IQuestionService questionService,
            IFeedbackService feedbackService,
            IPresentationService presentationService,
            IMeetupService meetupService,
            IMapper mapper
            )
        {
            _questionService = questionService;
            _presentationService = presentationService;
            _feedbackService = feedbackService;
            _meetupService = meetupService;
            _mapper = mapper;
        }

        /// <summary>
        /// Endpoint to get questions for a specific presentations on the specified meetup (by meetup id and by presentation id) 
        /// </summary>
        /// <param name="presentationId">Id of presentation (Guid) </param>
        /// <param name="meetupId">Id of meetup (Guid) </param>
        /// <returns></returns>
        [HttpGet("{meetupId}/presentations/{presentationId}/questions")]
        public async Task<List<QuestionModel>> GetQuestionsByMeetupPresentation(Guid meetupId, Guid presentationId)
        {
            var questionsDto = await _questionService.GetByMeetupPresentationIdAsync(meetupId, presentationId);

            return _mapper.Map<List<QuestionModel>>(questionsDto);
        }

        /// <summary>
        /// Endpoint to get feedbacks for a specified meetup (by meetup id) 
        /// </summary>
        /// <param name="meetupId">Id of meetup (Guid) </param>
        [HttpGet("{meetupId}/feedbacks")]
        public async Task<List<FeedbackModel>> GetFeedbacksByMeetup(Guid meetupId)
        {
            var feedbacksDto = await _feedbackService.GetByMeetupIdAsync(meetupId);

            return _mapper.Map<List<FeedbackModel>>(feedbacksDto);
        }

        /// <summary>
        /// Returns a list of all meetups
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<MeetupModel>> GetMeetups()
        {
            var meetupsDto = await _meetupService.GetAllAsync();
            return _mapper.Map<List<MeetupModel>>(meetupsDto);
        }
        
        /// <summary>
        /// Returns a list of all presentations for the specified meetup
        /// </summary>
        /// <returns></returns>
        [HttpGet("{meetupId}/presentations")]
        public async Task<List<PresentationModel>> GetPresentationsForMeetup(Guid meetupId)
        {
            var presentations = await _presentationService.GetAllAsync();
            return _mapper.Map<List<PresentationModel>>(presentations);
        }
    }
}
