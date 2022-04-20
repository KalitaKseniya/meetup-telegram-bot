using AutoMapper;
using meetup_telegram_bot.SignalR.Models;
using MeetupTelegramBot.BusinessLayer.Interfaces;
using MeetupTelegramBot.BusinessLayer.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace MeetupTelegramBot.BusinessLayer.Services
{
    public class InputInfo
    {
        public string UserState { get; set; }
        public string FeedbackGeneralFeedback { get; set; }
        public string QuestionText { get; set; }
    }

    public class ClientStatesService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiUrl;

        private List<PresentationModel> presentations;
        public List<PresentationModel> Presentations 
        {
            get
            {
                if (presentations == null)
                {
                    Task.Run(() => SetPresentationsAsync()).Wait();
                }
                return presentations;
            }
            private set
            {
                presentations = value;
            }
        }
        public const string LeaveFeedback = "Оставить отзыв";
        public Dictionary<long, InputInfo> ClientStates { get; private set; }

        public ClientStatesService(IConfiguration configuration)
        {
            ClientStates = new Dictionary<long, InputInfo>();
            _httpClient = new HttpClient();

            _apiUrl = configuration.GetSection("environmentVariables")["ApiUrl"];
            if (string.IsNullOrEmpty(_apiUrl))
            {
                throw new Exception("Not found api url in configuration.");
            }
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

        public async Task SetPresentationsAsync()
        {
            presentations = await LoadPresentationsAsync();
        }

        public async Task<List<PresentationModel>> LoadPresentationsAsync()
        {
            var serverResponse = await _httpClient.GetAsync(_apiUrl + "api/presentations");

            if (serverResponse.IsSuccessStatusCode)
            {
                var stringContent = await serverResponse.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<List<PresentationModel>>(stringContent) ?? new List<PresentationModel>();
            }

            return new List<PresentationModel>();
        }
    }
}
