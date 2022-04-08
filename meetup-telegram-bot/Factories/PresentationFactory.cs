using meetup_telegram_bot.Controllers.Boundary.Request;
using meetup_telegram_bot.Data.DbEntities;
using meetup_telegram_bot.SignalR.Models;

namespace meetup_telegram_bot.Factories
{
    public static class PresentationFactory
    {
        public static PresentationModel ToModel(this PresentationDbEntity dbEntity)
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

        public static List<PresentationModel> ToModel(this List<PresentationDbEntity> dbEntities)
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
