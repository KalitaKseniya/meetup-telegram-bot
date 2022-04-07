using MeetupTelegramBot.BusinessLayer.Models.DTO;

namespace MeetupTelegramBot.BusinessLayer.Interfaces
{
    public interface INotificationService
    {
        public Task SendFeedbackAsync(FeedbackDTO feedback);
        public Task SendQuestionAsync(QuestionDTO question);
    }
}
