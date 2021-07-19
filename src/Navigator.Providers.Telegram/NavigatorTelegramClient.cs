using System.Threading;
using System.Threading.Tasks;
using Navigator.Configuration;
using Navigator.Entities;
using Telegram.Bot;

namespace Navigator.Providers.Telegram
{
    public class NavigatorTelegramClient : TelegramBotClient, INavigatorClient
    {
        public NavigatorTelegramClient(INavigatorOptions options) 
            : base(options.GetTelegramToken())
        {
        }

        public async Task<BotUser> GetProfile(CancellationToken cancellationToken = default)
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