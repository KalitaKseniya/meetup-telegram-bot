namespace MeetupTelegramBot.DataAccess.Entities
{
    public class QuestionEntity
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public string Text { get; set; }
        public Guid PresentationId { get; set; }
        public string AuthorName { get; set; }
    }
}
