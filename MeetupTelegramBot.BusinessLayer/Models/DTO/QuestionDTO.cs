namespace MeetupTelegramBot.BusinessLayer.Models.DTO;

public class QuestionDTO
{
    public Guid Id { get; set; }
    public TimeSpan Time { get; set; }
    public string Text { get; set; }
    public string AuthorName { get; set; }
    public Guid MeetupId { get; set; }
    public Guid PresentationId { get; set; }
}