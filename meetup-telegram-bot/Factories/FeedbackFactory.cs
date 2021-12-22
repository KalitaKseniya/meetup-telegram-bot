using meetup_telegram_bot.Data.DbEntities;
using meetup_telegram_bot.SignalR.Models;

namespace meetup_telegram_bot.Factories
{
    public static class FeedbackFactory
    {
        public static FeedbackModel ToModel(this FeedbackDbEntity dbEntity)
        {
            return dbEntity == null ? new FeedbackModel() : new FeedbackModel
            {
                AuthorName = dbEntity.AuthorName,
                FutureProposal = dbEntity.FutureProposal,
                Message = dbEntity.GeneralFeedback,
                Sent = dbEntity.Date.Add(dbEntity.Time)
            };
        }

        public static List<FeedbackModel> ToModel(this List<FeedbackDbEntity> dbEntities)
        {
            return dbEntities == null ? new List<FeedbackModel>() :
                dbEntities.Select(x => x.ToModel()).ToList();
        }
    }
}
