namespace MeetupTelegramBot.BusinessLayer.SignalR.Models
{
    public class MeetupModel
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public string Place { get; set; }
    }
}
