using System.ComponentModel.DataAnnotations;

namespace MeetupTelegramBot.BusinessLayer.Models.DTO.Request
{
    public class PresentationForUpdateDto
    {
        [Required]
        public Guid Id { get; set; }
    }
}
