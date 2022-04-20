using meetup_telegram_bot.SignalR.Models;
using MeetupTelegramBot.BusinessLayer.Models.DTO;

namespace MeetupTelegramBot.BusinessLayer.Factories
{
    public static class FeedbackFactory
    {
        public static FeedbackModel ToModel(this FeedbackDTO dbEntity)
        {
            return dbEntity == null ? new FeedbackModel() : new FeedbackModel
            {
                AuthorName = dbEntity.AuthorName,
                FutureProposal = dbEntity.FutureProposal,
                Message = dbEntity.GeneralFeedback,
                Sent = dbEntity.Sent
            };
        }

        public static List<FeedbackModel> ToModel(this List<FeedbackDTO> dbEntities)
        {
            return dbEntities == null ? new List<FeedbackModel>() :
                dbEntities.Select(x => x.ToModel()).ToList();
        }
    }
}
