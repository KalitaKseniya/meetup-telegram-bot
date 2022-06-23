namespace MeetupTelegramBot.DataAccess.Entities
{
    public class PresentationEntity
    {
        public Guid Id { get; set; }
        public Guid SpeackerId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public SpeackerEntity Speacker { get; set; }
        public ICollection<MeetupEntity> Meetups { get; set; }
        public ICollection<MeetupPresentationEntity> MeetupPresentations { get; set; }
    }
}
