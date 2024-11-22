using Microsoft.AspNetCore.Http.Json;
using Microsoft.Extensions.DependencyInjection;
using Navigator.Abstractions.Actions.Arguments;
using Navigator.Abstractions.Classifier;
using Navigator.Abstractions.Client;
using Navigator.Abstractions.Pipelines.Builder;
using Navigator.Abstractions.Pipelines.Steps;
using Navigator.Abstractions.Strategies;
using Navigator.Actions.Arguments;
using Navigator.Actions.Arguments.Resolvers;
using Navigator.Catalog.Factory;
using Navigator.Client;
using Navigator.Configuration;
using Navigator.Configuration.Options;
using Navigator.Hosted;
using Navigator.Pipelines.Builder;
using Navigator.Pipelines.Steps;
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
    /// <param name="configuration"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static NavigatorConfiguration AddNavigator(this IServiceCollection services, Action<NavigatorConfiguration> configuration)
    {
        var navigatorConfiguration = BuildConfiguration(configuration);

        services.AddScoped<INavigatorClient, NavigatorClient>();
        services.AddSingleton<BotActionCatalogFactory>();

        services.AddScoped<IUpdateClassifier, UpdateClassifier>();

        services.AddArgumentProvider();
        
        services.AddNavigatorPipeline(navigatorConfiguration);

        services.AddScoped<INavigatorStrategy, NavigatorStrategy>();

        services.AddHostedService<SetTelegramBotWebHookHostedService>();

        services.ConfigureTelegramBot<JsonOptions>(opt => opt.SerializerOptions);

        return navigatorConfiguration;
    }

    private static NavigatorConfiguration BuildConfiguration(Action<NavigatorConfiguration> configurationBuilder)
    {
        ArgumentNullException.ThrowIfNull(configurationBuilder);
        
        var configuration = new NavigatorConfiguration();
        configurationBuilder(configuration);

        return configuration;
    }

    private static void AddArgumentProvider(this IServiceCollection services)
    {
        services.AddScoped<IActionArgumentProvider, ActionArgumentProvider>();

        services.AddScoped<IArgumentResolver, NavigatorEntitiesArgumentResolver>();
        services.AddScoped<IArgumentResolver, TelegramEntitiesArgumentResolver>();
        services.AddScoped<IArgumentResolver, TelegramMessageArgumentResolver>();
        services.AddScoped<IArgumentResolver, TelegramUpdateArgumentResolver>();
    }

    private static void AddNavigatorPipeline(this IServiceCollection services, NavigatorConfiguration configuration)
    {
        services.AddScoped<INavigatorPipelineStep, DefaultActionResolutionMainStep>();
        services.AddScoped<INavigatorPipelineStep, DefaultActionExecutionMainStep>();

        if (configuration.Options.MultipleActionsUsageIsEnabled() == false)
            services.AddScoped<INavigatorPipelineStep, FilterByMultipleActionsPipelineStep>();

        if (configuration.Options.ChatActionNotificationIsEnabled())
            services.AddScoped<INavigatorPipelineStep, ChatActionInExecutionPipelineStep>();

        services.AddScoped<INavigatorPipelineStep, FilterByConditionInResolutionPipelineStep>();

        services.AddScoped<INavigatorPipelineBuilder, DefaultNavigatorPipelineBuilder>();
    }
}