
using meetup_telegram_bot.Data;

namespace meetup_telegram_bot.Services
{
    public class InputInfo//ToDo: implement getters and setters so Only Feedback or Questions can be at the same time
    {
        public UserState UserState { get; set; }
        public string FeedbackGeneralFeedback { get; set; }
        public string QuestionText { get; set; }
    }

    public class ClientStatesService
    {
        public Dictionary<long, InputInfo> ClientStates { get; set; }
        public ClientStatesService()
        {
            ClientStates = new Dictionary<long, InputInfo>();
        }

        public void SetUserState(long chatId, UserState userState)
        {
            ClientStates[chatId] = new InputInfo
            {
                UserState = userState
            };
        }
        
        public void SetFeedback(long chatId, string generalFeedback)
        {
            if (!ClientStates.ContainsKey(chatId) || ClientStates[chatId].UserState != UserState.LeaveFeedback)
            {
                throw new Exception("Error in model");
            }
            ClientStates[chatId] = new InputInfo
            {
                UserState = UserState.LeaveFeedback,
                FeedbackGeneralFeedback = generalFeedback
            };
        }
        
        public void SetPresentationQuestion(long chatId, string questionText)
        {
            var userState = ClientStates[chatId].UserState;
            
            if (!ClientStates.ContainsKey(chatId) || (
                userState != UserState.FirstPresentationQuestion &&  
                userState != UserState.SecondPresentationQuestion &&
                userState != UserState.ThirdPresentationQuestion &&
                userState != UserState.OutOfPresentationQuestion)
            )
            {
                throw new Exception("Error in model");
            }

            ClientStates[chatId] = new InputInfo
            {
                UserState = userState,
                QuestionText = questionText
            };
        } 
    }
}
