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
                Description = dbEntity.Description,
                SpeackerName = dbEntity.SpeackerName,
                Title = dbEntity.Title
            };
        }

        public static List<PresentationModel> ToModel(this List<PresentationDbEntity> dbEntities)
        {
            return dbEntities == null ? new List<PresentationModel>() : 
                dbEntities.Select(x => x.ToModel()).ToList();
        }
    }
}
