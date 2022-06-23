namespace MeetupTelegramBot.DataAccess.Entities
{
    public class MeetupEntity
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public string Place { get; set; }
        public ICollection<PresentationEntity> Presentations { get; set; }
        public ICollection<MeetupPresentationEntity> MeetupPresentations { get; set; }
        public ICollection<FeedbackEntity> Feedbacks { get; set; }
    }
}
