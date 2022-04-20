using meetup_telegram_bot.SignalR.Models;
using MeetupTelegramBot.BusinessLayer.Models.DTO;

namespace MeetupTelegramBot.BusinessLayer.Factories
{
    public static class QuestionFactory
    {
        public static QuestionModel ToModel(this QuestionDTO dbEntity)
        {
            return dbEntity == null ? new QuestionModel() : new QuestionModel
            {
                Asked = dbEntity.Asked,
                AuthorName = dbEntity.AuthorName,
                PresentationId = dbEntity.PresentationId,
                QuestionText = dbEntity.Text
            };
        }

        public static List<QuestionModel> ToModel(this List<QuestionDTO> dbEntities)
        {
            return dbEntities == null ? new List<QuestionModel>() :
                dbEntities.Select(x => x.ToModel()).ToList();
        }
    }
}
