using Microsoft.Extensions.Logging;
using Navigator.Abstractions.Actions.Arguments;
using Navigator.Abstractions.Pipelines.Context;
using Navigator.Abstractions.Pipelines.Steps;

namespace Navigator.Pipelines.Steps;

/// <summary>
///     Default implementation of <see cref="IActionExecutionPipelineStep" />.
/// </summary>
public class DefaultActionExecutionStep : IActionExecutionPipelineStep
{
    private readonly IActionArgumentProvider _argumentProvider;
    private readonly ILogger<DefaultActionExecutionStep> _logger;

    /// <summary>
    ///     Initializes a new instance of the <see cref="DefaultActionExecutionStep" /> class.
    /// </summary>
    public DefaultActionExecutionStep(IActionArgumentProvider argumentProvider, ILogger<DefaultActionExecutionStep> logger)
    {
        _argumentProvider = argumentProvider;
        _logger = logger;
    }

    /// <inheritdoc />
    public async Task InvokeAsync(NavigatorActionExecutionContext context, PipelineStepHandlerDelegate next)
    {
        try
        {
            _logger.LogInformation("Executing action {ActionName} for update {UpdateId}", context.Action.Information.Name,
                context.Update.Id);

            var arguments = await _argumentProvider.GetArguments(context.Update, context.Action);

            await context.Action.ExecuteHandler(arguments);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to execute action {ActionName} for update {UpdateId}", context.Action.Information.Name,
                context.Update.Id);
        }
        finally
        {
            _logger.LogInformation("Finished executing action {ActionName} for update {UpdateId}", context.Action.Information.Name,
                context.Update.Id);
        }

        await next();
    }
}