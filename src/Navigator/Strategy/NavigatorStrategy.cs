using Microsoft.Extensions.Logging;
using Navigator.Abstractions.Pipelines.Builder;
using Navigator.Abstractions.Pipelines.Context;
using Navigator.Abstractions.Strategies;
using Telegram.Bot.Types;

namespace Navigator.Strategy;

/// <summary>
///     Navigator Strategy. Resolves <see cref="Update" /> and executes <see cref="BotAction" />.
/// </summary>
public class NavigatorStrategy : INavigatorStrategy
{
    private readonly ILogger<NavigatorStrategy> _logger;
    private readonly INavigatorPipelineBuilder _pipelineBuilder;

    /// <summary>
    ///     Initializes a new instance of the <see cref="NavigatorStrategy" /> class.
    /// </summary>
    public NavigatorStrategy(INavigatorPipelineBuilder pipelineBuilder, ILogger<NavigatorStrategy> logger)
    {
        _pipelineBuilder = pipelineBuilder;
        _logger = logger;
    }

    /// <inheritdoc />
    public async Task Invoke(Update update)
    {
        _logger.LogInformation("Processing update {UpdateId}", update.Id);

        var context = new NavigatorActionResolutionContext(update);

        var resolutionPipeline = _pipelineBuilder.BuildResolutionPipeline(context);

        _logger.LogInformation("Executing resolution pipeline for update {UpdateId}", update.Id);

        await resolutionPipeline.InvokeAsync();

        foreach (var executionContext in context.GetExecutionContexts())
        {
            var executionPipeline = _pipelineBuilder.BuildExecutionPipeline(executionContext);

            _logger.LogInformation("Executing execution pipeline for update {UpdateId} and action {ActionName}",
                update.Id, executionContext.Action.Information.Name);

            await executionPipeline.InvokeAsync();
        }

        _logger.LogInformation("Finished processing update {UpdateId}", update.Id);
    }
}