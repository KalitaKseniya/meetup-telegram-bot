using MeetupTelegramBot.BusinessLayer.Models.DTO;

namespace MeetupTelegramBot.BusinessLayer.Interfaces
{
    public interface IMeetupService
    {
        Task<List<MeetupDTO>> GetAllAsync();
    }
}
