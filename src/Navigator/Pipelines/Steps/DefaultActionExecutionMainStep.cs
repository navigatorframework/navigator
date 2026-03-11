using Microsoft.Extensions.Logging;
using Navigator.Abstractions.Actions.Arguments;
using Navigator.Abstractions.Introspection;
using Navigator.Abstractions.Pipelines.Context;
using Navigator.Abstractions.Pipelines.Steps;

namespace Navigator.Pipelines.Steps;

/// <summary>
///     Default implementation of <see cref="IActionExecutionMainStep" />.
/// </summary>
public class DefaultActionExecutionMainStep : IActionExecutionMainStep
{
    private readonly IActionArgumentProvider _argumentProvider;
    private readonly INavigatorTracerFactory<DefaultActionExecutionMainStep> _tracerFactory;
    private readonly ILogger<DefaultActionExecutionMainStep> _logger;

    /// <summary>
    ///     Initializes a new instance of the <see cref="DefaultActionExecutionMainStep" /> class.
    /// </summary>
    public DefaultActionExecutionMainStep(
        IActionArgumentProvider argumentProvider,
        INavigatorTracerFactory<DefaultActionExecutionMainStep> tracerFactory,
        ILogger<DefaultActionExecutionMainStep> logger)
    {
        _argumentProvider = argumentProvider;
        _tracerFactory = tracerFactory;
        _logger = logger;
    }

    /// <inheritdoc />
    public async Task InvokeAsync(NavigatorActionExecutionContext context, PipelineStepHandlerDelegate next)
    {
        await using var tracer = _tracerFactory.Get();

        try
        {
            _logger.LogInformation("Executing action {ActionName} for update {UpdateId}", context.Action.Information.Name,
                context.UpdateContext.Update.Id);

            var arguments = await _argumentProvider.GetHandlerArguments(context.UpdateContext.Update, context.Action);

            await context.Action.ExecuteHandler(arguments);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to execute action {ActionName} for update {UpdateId}", context.Action.Information.Name,
                context.UpdateContext.Update.Id);
        }
        finally
        {
            _logger.LogInformation("Finished executing action {ActionName} for update {UpdateId}", context.Action.Information.Name,
                context.UpdateContext.Update.Id);
        }

        await next();
    }
}