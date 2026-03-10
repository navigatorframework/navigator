using Microsoft.Extensions.Logging;
using Navigator.Abstractions.Actions;
using Navigator.Abstractions.Pipelines.Builder;
using Navigator.Abstractions.Pipelines.Context;
using Navigator.Abstractions.Strategies;
using Telegram.Bot.Types;

namespace Navigator.Strategy;

/// <summary>
///     Default navigator strategy. Resolves <see cref="Update" /> and executes <see cref="BotAction" /> sequentially.
/// </summary>
public class DefaultNavigationStrategy : INavigatorStrategy
{
    private readonly INavigatorPipelineBuilder _pipelineBuilder;
    private readonly INavigatorUpdateContextBuilder _contextBuilder;
    private readonly ILogger<DefaultNavigationStrategy> _logger;

    /// <summary>
    ///     Initializes a new instance of the <see cref="DefaultNavigationStrategy" /> class.
    /// </summary>
    public DefaultNavigationStrategy(INavigatorPipelineBuilder pipelineBuilder, INavigatorUpdateContextBuilder contextBuilder, ILogger<DefaultNavigationStrategy> logger)
    {
        _pipelineBuilder = pipelineBuilder;
        _contextBuilder = contextBuilder;
        _logger = logger;
    }

    /// <inheritdoc />
    public async Task Invoke(Update update, string identifier)
    {
        _logger.LogInformation("Processing update {UpdateId}", update.Id);

        var updateContext = await _contextBuilder.Build(update);
        
        var resolutionContext = new NavigatorActionResolutionContext(updateContext);

        var resolutionPipeline = _pipelineBuilder.BuildResolutionPipeline(resolutionContext);

        _logger.LogInformation("Executing resolution pipeline for update {UpdateId}", update.Id);

        await resolutionPipeline.InvokeAsync();

        foreach (var executionContext in resolutionContext.GetExecutionContexts())
        {
            var executionPipeline = _pipelineBuilder.BuildExecutionPipeline(executionContext);

            _logger.LogInformation("Executing execution pipeline for update {UpdateId} and action {ActionName}",
                update.Id, executionContext.Action.Information.Name);

            await executionPipeline.InvokeAsync();
        }

        _logger.LogInformation("Finished processing update {UpdateId}", update.Id);
    }
}