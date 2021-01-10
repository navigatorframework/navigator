using System.Net.Http;
using Microsoft.Extensions.Options;
using MihaZupan.TelegramBotClients;
using Navigator.Abstractions;
using Navigator.Configuration;

namespace Navigator
{
    /// <inheritdoc cref="Navigator.Abstractions.IBotClient" />
    public class BotClient : RateLimitedTelegramBotClient, IBotClient
    {
        /// <inheritdoc />
        public BotClient(IOptions<NavigatorOptions> options) : base(options.Value.BotToken, (HttpClient) default!, options.Value.SchedulerSettings)
        {
        }
    }
}