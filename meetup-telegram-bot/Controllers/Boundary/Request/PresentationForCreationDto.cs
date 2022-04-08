using System.ComponentModel.DataAnnotations;

namespace meetup_telegram_bot.Controllers.Boundary.Request
{
    public class PresentationForCreationDto
    {
        public string SpeackerName { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        [Required]
        public bool IsDisplayed { get; set; }
    }
}
