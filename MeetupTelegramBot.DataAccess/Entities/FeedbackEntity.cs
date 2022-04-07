namespace MeetupTelegramBot.DataAccess.Entities
{
    public class FeedbackEntity
    {
        public Guid Id { get; set; }
        public string AuthorName { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public string GeneralFeedback { get; set; }
        public string FutureProposal { get; set; }
    }
}
