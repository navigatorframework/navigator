using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using MihaZupan.TelegramBotClients;
using Navigator.Configuration;
using Navigator.Entities;

namespace Navigator.Provider.Telegram
{
    public class NavigatorTelegramClient : RateLimitedTelegramBotClient, INavigatorClient
    {
        public NavigatorTelegramClient(NavigatorOptions options) 
            : base(options.GetTelegramToken(), (HttpClient) default!, options.GetSchedulerSettingsOrDefault())
        {
        }

        public async Task<BotUser> GetBotUser(CancellationToken cancellationToken = default)
        {
            var bot = await GetMeAsync(cancellationToken);

            return new BotUser
            {
                Id = bot.Id.ToString(),
                Username = bot.Username
            };
        }
    }
}