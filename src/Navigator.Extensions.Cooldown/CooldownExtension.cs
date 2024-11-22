using Microsoft.Extensions.DependencyInjection;
using Navigator.Abstractions.Actions;
using Navigator.Abstractions.Extensions;
using Navigator.Abstractions.Pipelines.Steps;
using Navigator.Configuration.Options;
using Navigator.Extensions.Cooldown.Step;

namespace Navigator.Extensions.Cooldown;

/// <summary>
///     Extension for adding cooldowns to a <see cref="BotAction" />.
/// </summary>
public class CooldownExtension : INavigatorExtension
{
    public void Configure(IServiceCollection services, NavigatorOptions options)
    {
        services.AddScoped<INavigatorPipelineStep, FilterByActionsInCooldownPipelineStep>();
        services.AddScoped<INavigatorPipelineStep, SetCooldownForActionPipelineStep>();
    }
}