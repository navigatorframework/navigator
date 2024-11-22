using Microsoft.Extensions.DependencyInjection;
using Navigator.Abstractions.Actions;
using Navigator.Abstractions.Extensions;
using Navigator.Abstractions.Pipelines.Steps;
using Navigator.Configuration.Options;
using Navigator.Extensions.Probabilities.Step;

namespace Navigator.Extensions.Probabilities;

/// <summary>
///     Extension for adding probabilities of being executed to a <see cref="BotAction" />.
/// </summary>
public class ProbabilitiesExtension : INavigatorExtension
{
    /// <inheritdoc />
    public void Configure(IServiceCollection services, NavigatorOptions options)
    {
        services.AddSingleton<INavigatorPipelineStep, FilterByProbabilitiesStep>();
    }
}