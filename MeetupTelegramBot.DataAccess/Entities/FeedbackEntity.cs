namespace MeetupTelegramBot.DataAccess.Entities
{
    public class FeedbackEntity
    {
        public Guid Id { get; set; }
        public string AuthorName { get; set; }
        public TimeSpan Time { get; set; }
        public string GeneralFeedback { get; set; }
        public string FutureProposal { get; set; }
        public Guid MeetupId { get; set; }

        public MeetupEntity Meetup { get; set; }
    }
}
