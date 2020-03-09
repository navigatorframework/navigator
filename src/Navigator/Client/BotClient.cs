using System.Net;
using System.Net.Http;
using MihaZupan.TelegramBotClients;
using MihaZupan.TelegramBotClients.RateLimitedClient;
using Telegram.Bot;

namespace Navigator.Client
{
    public class BotClient : RateLimitedTelegramBotClient
    {
        public BotClient(ITelegramBotClient botClient, SchedulerSettings schedulerSettings = null) : base(botClient, schedulerSettings)
        {
        }

        public BotClient(string token, HttpClient httpClient = null, SchedulerSettings schedulerSettings = null) : base(token, httpClient, schedulerSettings)
        {
        }

        public BotClient(string token, IWebProxy webProxy, SchedulerSettings schedulerSettings = null) : base(token, webProxy, schedulerSettings)
        {
        }
    }
}