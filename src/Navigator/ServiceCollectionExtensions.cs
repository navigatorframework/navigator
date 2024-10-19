using Microsoft.AspNetCore.Http.Json;
using Microsoft.Extensions.DependencyInjection;
using Navigator.Abstractions.Actions.Arguments;
using Navigator.Actions.Arguments;
using Navigator.Actions.Arguments.Resolvers;
using Navigator.Catalog.Factory;
using Navigator.Client;
using Navigator.Configuration;
using Navigator.Configuration.Options;
using Navigator.Hosted;
using Navigator.Strategy;
using Navigator.Strategy.Classifier;

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

        services.AddScoped<IActionArgumentProvider, DefaultActionArgumentProvider>();

        services.AddScoped<IArgumentResolver, NavigatorEntitiesArgumentResolver>();
        services.AddScoped<IArgumentResolver, TelegramEntitiesArgumentResolver>();
        services.AddScoped<IArgumentResolver, TelegramMessageArgumentResolver>();
        services.AddScoped<IArgumentResolver, TelegramUpdateArgumentResolver>();

        services.AddTransient<INavigatorStrategy, NavigatorStrategy>();

        services.AddHostedService<SetTelegramBotWebHookHostedService>();

        navigatorBuilder.RegisterOrReplaceOptions();

        services.ConfigureTelegramBot<JsonOptions>(opt => opt.SerializerOptions);

        return navigatorBuilder;
    }
}