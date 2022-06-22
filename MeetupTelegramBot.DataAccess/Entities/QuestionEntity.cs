namespace MeetupTelegramBot.DataAccess.Entities
{
    public class QuestionEntity
    {
        public Guid Id { get; set; }
        public TimeSpan Time { get; set; }
        public string Text { get; set; }
        public Guid MeetupPresentationId { get; set; }
        public string AuthorName { get; set; }
        public MeetupPresentationEntity MeetupPresentation { get; set; }
    }
}
