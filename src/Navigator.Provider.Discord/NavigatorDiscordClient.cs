using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.Logging;
using Navigator.Configuration;

namespace Navigator.Provider.Discord
{
    public class NavigatorDiscordClient : DiscordShardedClient, INavigatorClient
    {
        private readonly ILogger<NavigatorDiscordClient> _logger;

        public NavigatorDiscordClient(INavigatorOptions options, ILogger<NavigatorDiscordClient> logger) 
            : base(new DiscordSocketConfig
            {
                TotalShards = options.GetTotalShardsOrDefault()
            })
        {
            _logger = logger;
            
            ShardReady += ReadyAsync;
            Log += LogAsync;
        }

        private Task ReadyAsync(DiscordSocketClient shard)
        {
            _logger.LogInformation("Navigator Discord Shard Number {ShardId} is connected and ready", shard.ShardId);
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