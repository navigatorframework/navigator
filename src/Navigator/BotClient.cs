using System.Net.Http;
using Microsoft.Extensions.Options;
using MihaZupan.TelegramBotClients;
using Navigator.Abstractions;

namespace Navigator
{
    /// <inheritdoc cref="Navigator.Abstractions.IBotClient" />
    public class BotClient : RateLimitedTelegramBotClient, IBotClient
    {
        /// <inheritdoc />
        public BotClient(NavigatorOptions options) : base(options.GetTelegramToken(), (HttpClient) default!, options.GetSchedulerSettingsOrDefault())
        {
        }
    }
}