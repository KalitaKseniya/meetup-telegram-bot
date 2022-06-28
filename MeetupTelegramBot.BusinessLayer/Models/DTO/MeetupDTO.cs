namespace MeetupTelegramBot.BusinessLayer.Models.DTO
{
    public class MeetupDTO
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public string Place { get; set; }
    }
}
