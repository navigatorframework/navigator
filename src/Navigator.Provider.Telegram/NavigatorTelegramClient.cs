using System.Net.Http;
using MihaZupan.TelegramBotClients;

namespace Navigator.Provider.Telegram
{
    public class NavigatorTelegramClient : RateLimitedTelegramBotClient, INavigatorClient
    {
        public NavigatorTelegramClient(NavigatorOptions options) 
            : base(options.GetTelegramToken(), (HttpClient) default!, options.GetSchedulerSettingsOrDefault())
        {
        }
    }
}