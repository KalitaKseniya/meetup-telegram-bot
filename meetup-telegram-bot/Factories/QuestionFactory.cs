using meetup_telegram_bot.Data.DbEntities;
using meetup_telegram_bot.SignalR.Models;

namespace meetup_telegram_bot.Factories
{
    public static class QuestionFactory
    {
        public static QuestionModel ToModel(this QuestionDbEntity dbEntity)
        {
            return dbEntity == null ? new QuestionModel() : new QuestionModel
            {
                Asked = dbEntity.Date.Add(dbEntity.Time),
                AuthorName = dbEntity.AuthorName,
                PresentationId = dbEntity.PresentationId,
                QuestionText = dbEntity.Text
            };
        }

        public static List<QuestionModel> ToModel(this List<QuestionDbEntity> dbEntities)
        {
            return dbEntities == null ? new List<QuestionModel>() :
                dbEntities.Select(x => x.ToModel()).ToList();
        }
    }
}
