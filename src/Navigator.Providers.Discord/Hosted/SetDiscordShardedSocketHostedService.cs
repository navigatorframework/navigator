using System;
using System.Threading;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Navigator.Abstractions;
using Navigator.Configuration;
using Navigator.Providers.Discord;

namespace Navigator.Provider.Telegram.Hosted
{
    /// <summary>
    /// WebHook service for navigator's telegram provider.
    /// </summary>
    public class SetDiscordShardedSocketHostedService : BackgroundService
    {
        private readonly ILogger<SetDiscordShardedSocketHostedService> _logger;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly NavigatorOptions _navigatorOptions;

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="serviceScopeFactory"></param>
        /// <param name="navigatorOptions"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public SetDiscordShardedSocketHostedService(ILogger<SetDiscordShardedSocketHostedService> logger, IServiceScopeFactory serviceScopeFactory,
            NavigatorOptions navigatorOptions)
        {
            _logger = logger;
            _serviceScopeFactory = serviceScopeFactory;
            _navigatorOptions = navigatorOptions;
        }

        /// <inheritdoc />
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            
            var navigatorClient = scope.ServiceProvider.GetRequiredService<NavigatorDiscordClient>();
            
            await navigatorClient.LoginAsync(TokenType.Bot, _navigatorOptions.GetDiscordToken());
            await navigatorClient.StartAsync();
            
            var me = navigatorClient.CurrentUser;

            if (me is not null)
            {
                _logger.LogInformation($"Navigator Discord Client is receiving updates for bot: @{me.Username}");
            }
            else
            {
                _logger.LogCritical("Unhandled error starting navigator discord client");
            }
        }
    }
}