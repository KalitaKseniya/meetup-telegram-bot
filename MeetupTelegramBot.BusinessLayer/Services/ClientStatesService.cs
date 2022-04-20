using AutoMapper;
using meetup_telegram_bot.SignalR.Models;
using MeetupTelegramBot.BusinessLayer.Interfaces;
using MeetupTelegramBot.BusinessLayer.Models;

namespace MeetupTelegramBot.BusinessLayer.Services
{
    public class InputInfo//ToDo: implement getters and setters so Only Feedback or Questions can be at the same time
    {
        public string UserState { get; set; }
        public string FeedbackGeneralFeedback { get; set; }
        public string QuestionText { get; set; }
    }

    public class ClientStatesService
    {
        private readonly IPresentationService _presentationService;
        private readonly IMapper _mapper;
        public List<PresentationModel> Presentations { get; private set; }
        public const string LeaveFeedback = "Оставить отзыв";
        public Dictionary<long, InputInfo> ClientStates { get; private set; }
        
        public ClientStatesService(IMapper mapper, IPresentationService presentationService)
        {
            _mapper = mapper;
            _presentationService = presentationService;
            ClientStates = new Dictionary<long, InputInfo>();
        }

        public void SetUserState(long chatId, string userState)
        {
            ClientStates[chatId] = new InputInfo
            {
                UserState = userState
            };
        }
        
        public void SetFeedback(long chatId, string generalFeedback)
        {
            if (!ClientStates.ContainsKey(chatId) || !IsFeedback(ClientStates[chatId].UserState))
            {
                throw new Exception("Error in model");
            }
            ClientStates[chatId] = new InputInfo
            {
                UserState = LeaveFeedback,
                FeedbackGeneralFeedback = generalFeedback
            };
        }
        
        public void SetPresentationQuestion(long chatId, string questionText)
        {
            var userState = ClientStates[chatId].UserState;
            
            if (!ClientStates.ContainsKey(chatId) || !IsPresentation(userState))
            {
                throw new Exception("Error in model");
            }

            ClientStates[chatId] = new InputInfo
            {
                UserState = userState,
                QuestionText = questionText
            };
        }
        
        public async Task ReloadDisplayedPresentations()
        {
            var presentationsDto = await _presentationService.GetDisplayedAsync();//ToDOo: call via httpl client?
            Presentations = _mapper.Map<List<PresentationModel>>(presentationsDto);
        }

        public bool IsPresentation(string presentationTitle)
        {
            return Presentations.Any(p => p.Title == presentationTitle);
        }

        public List<string> GetPresentations()
        {
            return Presentations.Select(p => p.Title).ToList();
        }

        public bool IsFeedback(string feedbackTitle)
        {
            return LeaveFeedback == feedbackTitle;
        }

        public Guid GetPresentationId(string title)
        {
            if (!IsPresentation(title))
            {
                throw new ArgumentException("No presentation with title " + title);
            }
            return Presentations.First(p => p.Title == title).Id;
        }

    }
}
