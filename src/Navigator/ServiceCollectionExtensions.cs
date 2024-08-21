using Microsoft.Extensions.DependencyInjection;
using Navigator.Catalog.Factory;
using Navigator.Client;
using Navigator.Configuration;
using Navigator.Configuration.Options;
using Navigator.Hosted;
using Navigator.Strategy;
using Navigator.Strategy.Classifier;
using Navigator.Strategy.TypeProvider;

namespace Navigator;

/// <summary>
///     Extensions for configuring Navigator.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    ///     Adds Navigator.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static NavigatorConfiguration AddNavigator(this IServiceCollection services, Action<NavigatorOptions> options)
    {
        if (options == null)
            throw new ArgumentNullException(nameof(options), "Navigator options are required for navigator framework to work.");

        var navigatorBuilder = new NavigatorConfiguration(options, services);

        services.AddScoped<INavigatorClient, NavigatorClient>();

        services.AddSingleton<BotActionCatalogFactory>();

        services.AddScoped<IUpdateClassifier, UpdateClassifier>();

        services.AddScoped<IArgumentTypeProvider, NavigatorEntitiesTypeProvider>();
        services.AddScoped<IArgumentTypeProvider, TelegramEntitiesTypeProvider>();
        services.AddScoped<IArgumentTypeProvider, TelegramMessageTypeProvider>();
        services.AddScoped<IArgumentTypeProvider, TelegramUpdateTypeProvider>();

        services.AddScoped<INavigatorStrategy, NavigatorStrategy>();

        services.AddHostedService<SetTelegramBotWebHookHostedService>();

        navigatorBuilder.RegisterOrReplaceOptions();

        return navigatorBuilder;
    }
}