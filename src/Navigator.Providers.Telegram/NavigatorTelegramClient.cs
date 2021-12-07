using System;
using System.Threading;
using System.Threading.Tasks;
using Navigator.Configuration;
using Navigator.Entities;
using Navigator.Providers.Telegram.Entities;
using Telegram.Bot;

namespace Navigator.Providers.Telegram
{
    public class NavigatorTelegramClient : TelegramBotClient, INavigatorClient
    {
        public NavigatorTelegramClient(INavigatorOptions options) 
            : base(options.GetTelegramToken() ?? throw new ArgumentNullException())
        {
        }

        public async Task<Bot> GetProfile(CancellationToken cancellationToken = default)
        {
            var bot = await this.GetMeAsync(cancellationToken);

            return new TelegramBot(bot.Id)
            {
                Username = bot.Username!
            };
        }
    }
}