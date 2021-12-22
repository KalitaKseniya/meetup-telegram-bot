using meetup_telegram_bot.SignalR.Interfaces;
using meetup_telegram_bot.SignalR.Models;
using Microsoft.AspNetCore.SignalR;

namespace meetup_telegram_bot.SignalR
{
    public class ChatHub : Hub<IChatHub>
    {
        public async Task SendFeedback(FeedbackModel feedback)
        {
            await Clients.All.SendFeedback(feedback).ConfigureAwait(false);
        }
        public async Task SendQuestion(QuestionModel question)
        {
            await Clients.All.SendQuestion(question).ConfigureAwait(false);
        }
        
        public async Task SendQuestions(List<QuestionModel> questions)
        {
            await Clients.All.SendQuestions(questions).ConfigureAwait(false);
        }
        
        public async Task SendFeedbacks(List<FeedbackModel> feedbacks)
        {
            await Clients.All.SendFeedbacks(feedbacks).ConfigureAwait(false);
        }
    }
}
