using System.Text.Json;

namespace MeetupTelegramBot.BusinessLayer.Models.DTO.Response
{
    public class BaseErrorResponse
    {
        public int StatusCode { get; private set; }
        public string Message { get; private set; }
        public string Details { get; private set; }
        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }

        public BaseErrorResponse(int statuscode, string message, string details)
        {
            StatusCode = statuscode;
            Message = message;
            Details = details;
        }
    }
}
