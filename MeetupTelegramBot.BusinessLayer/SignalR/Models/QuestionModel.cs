namespace meetup_telegram_bot.SignalR.Models
{
    public class QuestionModel
    {
        public string QuestionText { get; set; }
        public TimeSpan Asked { get; set; }
        public string AuthorName { get; set; }
    }
}
