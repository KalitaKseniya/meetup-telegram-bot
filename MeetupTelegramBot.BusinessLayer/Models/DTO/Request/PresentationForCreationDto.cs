using System.ComponentModel.DataAnnotations;

namespace MeetupTelegramBot.BusinessLayer.Models.DTO.Request
{
    public class PresentationForCreationDto
    {
        public Guid SpeackerId { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
