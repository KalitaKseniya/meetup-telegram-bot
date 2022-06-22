using MeetupTelegramBot.BusinessLayer.Factories;
using MeetupTelegramBot.BusinessLayer.Interfaces;
using MeetupTelegramBot.BusinessLayer.Models.DTO;
using MeetupTelegramBot.BusinessLayer.SignalR;
using MeetupTelegramBot.BusinessLayer.SignalR.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace MeetupTelegramBot.BusinessLayer.Services
{
    /// <summary>
    /// Service to send updates to all clients using SignalR
    /// </summary>
    public class NotificationService : INotificationService
    {
        private readonly IHubContext<ChatHub, IChatHub> _hubContext;

        public NotificationService(IHubContext<ChatHub, IChatHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task SendFeedbackAsync(FeedbackDTO feedbackEntity)
        {
            if (feedbackEntity == null)
            {
                throw new ArgumentNullException(nameof(feedbackEntity));
            }

            var feedback = feedbackEntity.ToModel(); 

            await _hubContext.Clients.All.SendFeedback(feedback);
        }
        
        public async Task SendQuestionAsync(QuestionDTO questionEntity)
        {
            if (questionEntity == null)
            {
                throw new ArgumentNullException(nameof(questionEntity));
            }

            var question = questionEntity.ToModel(); 

            await _hubContext.Clients.All.SendQuestion(question);
        }
    }
}
