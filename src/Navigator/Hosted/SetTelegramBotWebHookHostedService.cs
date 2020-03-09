using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Navigator.Client;
using Navigator.Configuration;

namespace Navigator.Hosted
{
    public class SetTelegramBotWebHookHostedService : BackgroundService
    {
        private readonly ILogger<SetTelegramBotWebHookHostedService> _logger;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly string _botWebHookUrl;
        
        public SetTelegramBotWebHookHostedService(ILogger<SetTelegramBotWebHookHostedService> logger, IServiceScopeFactory serviceScopeFactory,
            IOptions<NavigatorOptions> navigatorOptions)
        {
            _logger = logger;
            _serviceScopeFactory = serviceScopeFactory;

            if (string.IsNullOrWhiteSpace(navigatorOptions.Value.BotWebHookUrl))
            {
                throw new ArgumentNullException(nameof(navigatorOptions), "An URL for WebHook is required.");
            }
            
            _botWebHookUrl = navigatorOptions.Value.BotWebHookUrl;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogTrace("starting with setup of webhook.");
            _logger.LogTrace("Using url {WebHookUrl}", _botWebHookUrl);
            
            using var scope = _serviceScopeFactory.CreateScope();
            
            var botClient = scope.ServiceProvider.GetRequiredService<IBotClient>();
            
            await botClient.SetWebhookAsync(_botWebHookUrl, cancellationToken: stoppingToken);
            
            var me = await botClient.GetMeAsync(stoppingToken);

            _logger.LogInformation($"Telegram Bot Client is receiving updates for bot: @{me.Username} at the url: {_botWebHookUrl}");
        }
    }
}