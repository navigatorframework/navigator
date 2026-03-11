using Microsoft.Extensions.Logging;
using Navigator.Abstractions.Actions.Arguments;
using Navigator.Abstractions.Introspection;
using Navigator.Abstractions.Pipelines.Context;
using Navigator.Abstractions.Pipelines.Steps;

namespace Navigator.Pipelines.Steps;

/// <summary>
///     Discards actions that do not meet the condition.
/// </summary>
internal class FilterByConditionInResolutionPipelineStep : IActionResolutionPipelineStepAfter
{
    private readonly IActionArgumentProvider _argumentProvider;
    private readonly INavigatorTracerFactory<FilterByConditionInResolutionPipelineStep> _tracerFactory;
    private readonly ILogger<FilterByConditionInResolutionPipelineStep> _logger;

    /// <summary>
    ///     Initializes a new instance of the <see cref="FilterByConditionInResolutionPipelineStep" /> class.
    /// </summary>
    public FilterByConditionInResolutionPipelineStep(IActionArgumentProvider argumentProvider,
        INavigatorTracerFactory<FilterByConditionInResolutionPipelineStep> tracerFactory,
        ILogger<FilterByConditionInResolutionPipelineStep> logger)
    {
        _argumentProvider = argumentProvider;
        _tracerFactory = tracerFactory;
        _logger = logger;
    }

    /// <inheritdoc />
    public async Task InvokeAsync(NavigatorActionResolutionContext context, PipelineStepHandlerDelegate next)
    {
        await using var tracer = _tracerFactory.Get();
        tracer.AddTag(NavigatorTraceKeys.UpdateCategory, context.UpdateCategory.ToString());

        if (context.Actions.Count == 0)
        {
            await next();
            return;
        }

        for (var i = context.Actions.Count - 1; i >= 0; i--)
        {
            var arguments = await _argumentProvider.GetConditionArguments(context.UpdateContext.Update, context.Actions[i]);

            if (await context.Actions[i].ExecuteCondition(arguments)) continue;

            _logger.LogDebug("Discarding action {ActionName} because condition is not met", context.Actions[i].Information.Name);

            context.Actions.RemoveAt(i);
        }

        foreach (var action in context.Actions)
        {
            tracer.AddTag(NavigatorTraceKeys.ActionMatched, action.Information.Name);
        }

        await next();
    }
}