using meetup_telegram_bot.Controllers.Boundary.Request;
using meetup_telegram_bot.Data.DbEntities;
using meetup_telegram_bot.SignalR.Models;
using MeetupTelegramBot.BusinessLayer.Models.DTO;

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

        public static PresentationDbEntity ToDbEntity(this PresentationForCreationDto dbEntity)
        {
            return dbEntity == null ? new PresentationDbEntity() : new PresentationDbEntity
            {
                Description = dbEntity.Description,
                SpeackerName = dbEntity.SpeackerName,
                Title = dbEntity.Title,
                IsDisplayed = dbEntity.IsDisplayed,
            };
        }
    }
}
