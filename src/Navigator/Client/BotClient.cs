using System.Net;
using System.Net.Http;
using Microsoft.Extensions.Options;
using MihaZupan.TelegramBotClients;
using MihaZupan.TelegramBotClients.RateLimitedClient;
using Navigator.Configuration;
using Telegram.Bot;

namespace Navigator.Client
{
    public class BotClient : RateLimitedTelegramBotClient
    {
        public BotClient(IOptions<NavigatorOptions> options) : base(options.Value.BotToken, (HttpClient) null, options.Value.SchedulerSettings)
        {
        }
    }
}