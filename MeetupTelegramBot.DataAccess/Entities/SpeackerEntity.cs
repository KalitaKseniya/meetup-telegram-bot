namespace MeetupTelegramBot.DataAccess.Entities
{
    public class SpeackerEntity
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public ICollection<PresentationEntity> Presentations { get; set; }
    }
}
