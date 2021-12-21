using Microsoft.AspNetCore.SignalR;

namespace meetup_telegram_bot.Infrastructure
{
    public interface IChatHub
    {
        Task Send(string message);
    }
    public class ChatHub : Hub<IChatHub>
    {
        public async Task Send(string message)
        {
            await this.Clients.All.Send(message).ConfigureAwait(false);
        }
    }
}
