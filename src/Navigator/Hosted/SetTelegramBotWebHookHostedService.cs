using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Navigator.Abstractions;

namespace Navigator.Hosted
{
    /// <summary>
    /// WebHook service for Navigator.
    /// </summary>
    public class SetTelegramBotWebHookHostedService : BackgroundService
    {
        private readonly ILogger<SetTelegramBotWebHookHostedService> _logger;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly string _webHookUrl;
        
        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="serviceScopeFactory"></param>
        /// <param name="navigatorOptions"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public SetTelegramBotWebHookHostedService(ILogger<SetTelegramBotWebHookHostedService> logger, IServiceScopeFactory serviceScopeFactory,
            NavigatorOptions navigatorOptions)
        {
            _logger = logger;
            _serviceScopeFactory = serviceScopeFactory;

            if (string.IsNullOrWhiteSpace(navigatorOptions.GetWebHookBaseUrl()))
            {
                throw new ArgumentNullException(nameof(navigatorOptions), "An URL for WebHook is required.");
            }
            
            _webHookUrl = $"{navigatorOptions.GetWebHookBaseUrl()}/{navigatorOptions.GetWebHookEndpointOrDefault()}";
        }

        /// <inheritdoc />
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogTrace("Starting with setup of webhook.");
            _logger.LogTrace("Using webHook url {WebHookUrl}", _webHookUrl);
            
            using var scope = _serviceScopeFactory.CreateScope();
            
            var botClient = scope.ServiceProvider.GetRequiredService<IBotClient>();
            
            await botClient.SetWebhookAsync(_webHookUrl, cancellationToken: stoppingToken);
            
            var me = await botClient.GetMeAsync(stoppingToken);

            _logger.LogInformation($"Telegram Bot Client is receiving updates for bot: @{me.Username} at the url: {_webHookUrl}");
        }
    }
}