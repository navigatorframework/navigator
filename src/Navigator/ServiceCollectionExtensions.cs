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
using Navigator.Pipelines.Steps.Bundled;
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
        ArgumentNullException.ThrowIfNull(configuration);

        var navigatorConfiguration = new NavigatorConfiguration();
        configuration(navigatorConfiguration);

        services.AddScoped<INavigatorClient, NavigatorClient>();
        services.AddSingleton<BotActionCatalogFactory>();

        services.AddScoped<IUpdateClassifier, UpdateClassifier>();

        services.AddScoped<IActionArgumentProvider, ActionArgumentProvider>();

        services.AddScoped<IArgumentResolver, NavigatorEntitiesArgumentResolver>();
        services.AddScoped<IArgumentResolver, TelegramEntitiesArgumentResolver>();
        services.AddScoped<IArgumentResolver, TelegramMessageArgumentResolver>();
        services.AddScoped<IArgumentResolver, TelegramUpdateArgumentResolver>();

        services.AddScoped<INavigatorPipelineStep, DefaultActionResolutionMainStep>();
        services.AddScoped<INavigatorPipelineStep, DefaultActionExecutionMainStep>();

        if (navigatorConfiguration.Options.MultipleActionsUsageIsEnabled() == false)
            services.AddScoped<INavigatorPipelineStep, FilterByMultipleActionsPipelineStep>();

        if (navigatorConfiguration.Options.ChatActionNotificationIsEnabled())
            services.AddScoped<INavigatorPipelineStep, ChatActionInExecutionPipelineStep>();

        services.AddScoped<INavigatorPipelineStep, FilterByConditionInResolutionPipelineStep>();
        services.AddScoped<INavigatorPipelineStep, FilterByActionsInCooldownPipelineStep>();
        services.AddScoped<INavigatorPipelineStep, SetCooldownForActionPipelineStep>();

        services.AddScoped<INavigatorPipelineBuilder, DefaultNavigatorPipelineBuilder>();

        services.AddScoped<INavigatorStrategy, NavigatorStrategy>();

        services.AddHostedService<SetTelegramBotWebHookHostedService>();

        services.ConfigureTelegramBot<JsonOptions>(opt => opt.SerializerOptions);

        return navigatorConfiguration;
    }
}