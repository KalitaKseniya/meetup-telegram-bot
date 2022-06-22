namespace MeetupTelegramBot.DataAccess.Entities
{
    public class PresentationEntity
    {
        public Guid Id { get; set; }
        public string SpeackerName { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsDisplayed { get; set; }
    }
}
