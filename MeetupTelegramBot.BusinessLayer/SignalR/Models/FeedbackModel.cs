namespace meetup_telegram_bot.SignalR.Models
{
    public class FeedbackModel
    {
        public string AuthorName { get; set; }
        public string Message { get; set; }
        public string FutureProposal { get; set; }
        public TimeSpan Sent { get; set; }
    }
}
