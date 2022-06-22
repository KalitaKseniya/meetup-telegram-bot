namespace MeetupTelegramBot.DataAccess.Entities
{
    public class MeetupPresentationEntity
    {
        public Guid Id { get; set; }
        public Guid PresentationId { get; set; }
        public Guid MeetupId { get; set; }

        public PresentationEntity Presentation { get; set; }
        public MeetupEntity Meetup { get; set; }
    }
}
