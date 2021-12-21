using meetup_telegram_bot.Data.DbEntities;
using meetup_telegram_bot.Infrastructure.Interfaces;
using meetup_telegram_bot.SignalR;
using meetup_telegram_bot.SignalR.Interfaces;
using meetup_telegram_bot.SignalR.Models;
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

            var feedback = new FeedbackModel
            {
                FutureProposal = feedbackEntity.FutureProposal,
                Message = feedbackEntity.GeneralFeedback,
                Sent = feedbackEntity.Date.Add(feedbackEntity.Time)
            };

            await _hubContext.Clients.All.SendFeedback(feedback).ConfigureAwait(false);
        }

        public async Task SendQuestionAsync(QuestionDbEntity questionEntity)
        {
            if (questionEntity == null)
            {
                throw new ArgumentNullException(nameof(questionEntity));
            }

            var question = new QuestionModel
            {
                Asked = questionEntity.Date.Add(questionEntity.Time),
                AuthorName = questionEntity.AuthorName,
                PresentationId = questionEntity.PresentationId,
                QuestionText = questionEntity.Text
            };

            await _hubContext.Clients.All.SendQuestion(question).ConfigureAwait(false);
        }
    }
}
