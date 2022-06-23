using meetup_telegram_bot.SignalR.Models;
using MeetupTelegramBot.BusinessLayer.Models.DTO;
using MeetupTelegramBot.BusinessLayer.Models.DTO.Request;
using MeetupTelegramBot.DataAccess.Entities;

namespace MeetupTelegramBot.BusinessLayer.Factories
{
    public static class PresentationFactory
    {
        public static PresentationModel ToModel(this PresentationDTO dbEntity)
        {
            return dbEntity == null? new PresentationModel() : new PresentationModel
            {
                Id = dbEntity.Id,
                Description = dbEntity.Description,
                SpeackerName = dbEntity.SpeackerName,
                Title = dbEntity.Title,
                IsDisplayed = dbEntity.IsDisplayed
            };
        }

        public static List<PresentationModel> ToModel(this List<PresentationDTO> dbEntities)
        {
            return dbEntities == null ? new List<PresentationModel>() : 
                dbEntities.Select(x => x.ToModel()).ToList();
        }

        public static PresentationEntity ToDbEntity(this PresentationForCreationDto dbEntity)
        {
            return dbEntity == null ? new PresentationEntity() : new PresentationEntity
            {
                Description = dbEntity.Description,
                //SpeackerId = dbEntity.SpeackerName,
                Title = dbEntity.Title,
                //IsDisplayed = dbEntity.IsDisplayed,
            };
        }
    }
}
