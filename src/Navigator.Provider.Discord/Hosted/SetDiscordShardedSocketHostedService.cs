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
            
            var navigatorClient = scope.ServiceProvider.GetRequiredService<DiscordShardedClient>();
            
            navigatorClient.ShardReady += ReadyAsync;
            navigatorClient.Log += LogAsync;
            
            await navigatorClient.LoginAsync(TokenType.Bot, _navigatorOptions.GetDiscordToken());
            await navigatorClient.StartAsync();
            
            var me = await navigatorClient.CurrentUser;

            _logger.LogInformation($"Navigator Discord Client is receiving updates for bot: @{me.Username}");
        }
        
        private Task ReadyAsync(DiscordSocketClient shard)
        {
            _logger.LogInformation("Navigator Discord Shard Number {ShardId} is connected and ready!", shard.ShardId);
            return Task.CompletedTask;
        }

        private Task LogAsync(LogMessage log)
        {
            switch (log.Severity)
            {
                case LogSeverity.Critical:
                    _logger.LogCritical(log.Exception, "Message: {Message} Source: {Source}", log.Message, log.Source);
                    break;
                case LogSeverity.Error:
                    _logger.LogError(log.Exception, "Message: {Message} Source: {Source}", log.Message, log.Source);
                    break;
                case LogSeverity.Warning:
                    _logger.LogWarning(log.Exception, "Message: {Message} Source: {Source}", log.Message, log.Source);
                    break;
                case LogSeverity.Info:
                    _logger.LogInformation("Message: {Message} Source: {Source}", log.Message, log.Source);
                    break;
                case LogSeverity.Verbose:
                    _logger.LogTrace("Message: {Message} Source: {Source}", log.Message, log.Source);
                    break;
                case LogSeverity.Debug:
                    _logger.LogDebug("Message: {Message} Source: {Source}", log.Message, log.Source);
                    break;
                default:
                    _logger.LogInformation("Message: {Message} Source: {Source}", log.Message, log.Source);
                    break;
            }
            
            return Task.CompletedTask;
        }
    }
}