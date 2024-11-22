using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Navigator.Abstractions.Client;
using Navigator.Client;
using Navigator.Configuration.Options;
using Telegram.Bot;

namespace Navigator.Hosted;

/// <summary>
/// WebHook service for navigator's telegram provider.
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
        IOptions<NavigatorOptions> navigatorOptions)
    {
        _logger = logger;
        _serviceScopeFactory = serviceScopeFactory;
        
        var options = navigatorOptions.Value;

        if (string.IsNullOrWhiteSpace(options.GetWebHookBaseUrl()))
        {
            throw new ArgumentNullException(nameof(options), "An URL for WebHook is required.");
        }
            
        _webHookUrl = $"{options.GetWebHookBaseUrl()}/{options.GetWebHookEndpointOrDefault()}";
    }

    /// <inheritdoc />
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogTrace("Starting webhook setup for Navigator Telegram Bot");
        _logger.LogTrace("Using webhook url {WebHookUrl}", _webHookUrl);
            
        using var scope = _serviceScopeFactory.CreateScope();
            
        var navigatorClient = scope.ServiceProvider.GetRequiredService<INavigatorClient>();
            
        await navigatorClient.SetWebhookAsync(_webHookUrl, maxConnections: 100, cancellationToken: stoppingToken);
        
        _logger.LogTrace("Webhook configured successfully");
        
        var me = await navigatorClient.GetProfile(stoppingToken);

        _logger.LogInformation("Navigator is receiving updates for bot: @{Username} at the url: {WebHookUrl}", me.Username, _webHookUrl);
    }
}