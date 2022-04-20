using MeetupTelegramBot.BusinessLayer.Models.DTO.Response;
using MeetupTelegramBot.DataAccess.Exceptions;
using Newtonsoft.Json;
using System.Net;

namespace meetup_telegram_bot.Infrastructure
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;

        public ExceptionMiddleware(RequestDelegate next,
            ILogger<ExceptionMiddleware> logger,
            IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (ArgumentNullException ex)
            {
                await HandleExceptionAsync(context, ex, HttpStatusCode.BadRequest);
            }
            catch (ArgumentException ex)
            {
                await HandleExceptionAsync(context, ex, HttpStatusCode.BadRequest);
            }
            catch (EntityNotFoundException ex)
            {
                await HandleExceptionAsync(context, ex, HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex, HttpStatusCode.InternalServerError);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex, HttpStatusCode code)
        {
            _logger.LogError(ex, ex.StackTrace);

            var response = context.Response;
            response.ContentType = "application/json";
            response.StatusCode = (int)code;
            var allMessageText = ex.Message;

            var details = _env.IsDevelopment() && code == HttpStatusCode.InternalServerError
                ? ex.StackTrace?.ToString() :
                  string.Empty;

            await response.WriteAsync(JsonConvert.SerializeObject(new BaseErrorResponse((int)code, allMessageText, details)))
                    ;
        }
    }
}
