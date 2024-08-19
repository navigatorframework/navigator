using Microsoft.Extensions.DependencyInjection;
using Navigator.Catalog;
using Navigator.Client;
using Navigator.Configuration;
using Navigator.Hosted;
using Navigator.Strategy;
using Navigator.Strategy.Classifier;
using Scrutor;

namespace Navigator;

/// <summary>
/// Extensions for configuring Navigator.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds Navigator.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static NavigatorConfiguration AddNavigator(this IServiceCollection services, Action<NavigatorOptions> options)
    {
        if (options == null)
        {
            throw new ArgumentNullException(nameof(options), "Navigator options are required for navigator framework to work.");
        }

        var navigatorBuilder = new NavigatorConfiguration(options, services);

        services.AddScoped<INavigatorClient, NavigatorClient>();

        services.AddSingleton<IBotActionCatalogFactory, BotActionCatalogFactory>();

        services.AddScoped<IUpdateClassifier, UpdateClassifier>();
        services.AddScoped<INavigatorStrategy, NavigatorStrategy>();
        
        services.AddHostedService<SetTelegramBotWebHookHostedService>();

        navigatorBuilder.RegisterOrReplaceOptions();

        return navigatorBuilder;
    }
}