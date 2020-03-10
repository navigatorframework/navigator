using System.Net.Http;
using Microsoft.Extensions.Options;
using MihaZupan.TelegramBotClients;
using Navigator.Abstraction;
using Navigator.Configuration;

namespace Navigator
{
    public class BotClient : RateLimitedTelegramBotClient, IBotClient
    {
        public BotClient(IOptions<NavigatorOptions> options) : base(options.Value.BotToken, (HttpClient) null, options.Value.SchedulerSettings)
        {
        }
    }
}