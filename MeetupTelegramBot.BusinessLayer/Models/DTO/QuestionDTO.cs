namespace MeetupTelegramBot.BusinessLayer.Models.DTO;

public class QuestionDTO
{
    public Guid Id { get; set; }
    public DateTime Date { get; set; }
    public TimeSpan Time { get; set; }
    public string Text { get; set; }
    public Guid PresentationId { get; set; }
    public string AuthorName { get; set; }
}