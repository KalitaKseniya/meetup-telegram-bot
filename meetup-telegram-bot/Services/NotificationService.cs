using meetup_telegram_bot.Data.DbEntities;
using meetup_telegram_bot.Factories;
using meetup_telegram_bot.Infrastructure.Interfaces;
using meetup_telegram_bot.SignalR;
using meetup_telegram_bot.SignalR.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace meetup_telegram_bot.Services
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

        public async Task SendFeedbackAsync(FeedbackDbEntity feedbackEntity)
        {
            if (feedbackEntity == null)
            {
                throw new ArgumentNullException(nameof(feedbackEntity));
            }

            var feedback = feedbackEntity.ToModel(); 

            await _hubContext.Clients.All.SendFeedback(feedback).ConfigureAwait(false);
        }
        
        public async Task SendQuestionAsync(QuestionDbEntity questionEntity)
        {
            if (questionEntity == null)
            {
                throw new ArgumentNullException(nameof(questionEntity));
            }

            var question = questionEntity.ToModel(); 

            await _hubContext.Clients.All.SendQuestion(question).ConfigureAwait(false);
        }
    }
}
