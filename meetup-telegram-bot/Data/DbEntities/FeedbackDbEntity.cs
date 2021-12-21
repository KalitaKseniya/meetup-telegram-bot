namespace meetup_telegram_bot.Data.DbEntities
{
    public class FeedbackDbEntity
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public string GeneralFeedback { get; set; }
        public string FutureProposal { get; set; }
    }
}
