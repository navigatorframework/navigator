using Microsoft.Extensions.Logging;
using Navigator.Abstractions.Actions;
using Navigator.Abstractions.Introspection;
using Navigator.Abstractions.Pipelines.Builder;
using Navigator.Abstractions.Pipelines.Context;
using Navigator.Abstractions.Strategies;
using Navigator.Abstractions.Telegram;
using Telegram.Bot.Types;

namespace Navigator.Strategy;

/// <summary>
///     Default navigator strategy. Resolves <see cref="Update" /> and executes <see cref="BotAction" /> sequentially.
/// </summary>
public class DefaultNavigationStrategy : INavigatorStrategy
{
    private readonly INavigatorResolutionPipelineBuilder _resolutionPipelineBuilder;
    private readonly INavigatorExecutionPipelineBuilder _executionPipelineBuilder;
    private readonly INavigatorUpdateContextBuilder _contextBuilder;
    private readonly INavigatorTracerFactory<DefaultNavigationStrategy> _navigatorTracerFactory;
    private readonly ILogger<DefaultNavigationStrategy> _logger;

    /// <summary>
    ///     Initializes a new instance of the <see cref="DefaultNavigationStrategy" /> class.
    /// </summary>
    public DefaultNavigationStrategy(
        INavigatorResolutionPipelineBuilder resolutionPipelineBuilder,
        INavigatorExecutionPipelineBuilder executionPipelineBuilder,
        INavigatorUpdateContextBuilder contextBuilder,
        INavigatorTracerFactory<DefaultNavigationStrategy> navigatorTracerFactory,
        ILogger<DefaultNavigationStrategy> logger)
    {
        _resolutionPipelineBuilder = resolutionPipelineBuilder;
        _executionPipelineBuilder = executionPipelineBuilder;
        _contextBuilder = contextBuilder;
        _navigatorTracerFactory = navigatorTracerFactory;
        _logger = logger;
    }

    /// <inheritdoc />
    public async Task Invoke(Update update, string identifier)
    {
        await using var tracer = _navigatorTracerFactory.Get(identifier);
        AugmentTrace(tracer, update);

        _logger.LogInformation("Processing update {UpdateId}", update.Id);

        try
        {
            var updateContext = await _contextBuilder.Build(update);
        
            var resolutionContext = new NavigatorActionResolutionContext(updateContext);

            var resolutionPipeline = await _resolutionPipelineBuilder.BuildResolutionPipeline(resolutionContext);

            _logger.LogInformation("Executing resolution pipeline for update {UpdateId}", update.Id);

            await resolutionPipeline.InvokeAsync();

            foreach (var executionContext in resolutionContext.GetExecutionContexts())
            {
                var executionPipeline = await _executionPipelineBuilder.BuildExecutionPipeline(executionContext);

                _logger.LogInformation("Executing execution pipeline for update {UpdateId} and action {ActionName}",
                    update.Id, executionContext.Action.Information.Name);

                await executionPipeline.InvokeAsync();
            }
        }
        catch (Exception e)
        {
            tracer.SetError(e);
            throw;
        }

        _logger.LogInformation("Finished processing update {UpdateId}", update.Id);
    }

    private static void AugmentTrace(INavigatorTracer tracer, Update update)
    {
        tracer.AddTag(NavigatorTraceKeys.UpdateId, $"{update.Id}");

        var chat = update.GetChatOrDefault();
        if (chat is not null)
        {
            tracer.AddTag(NavigatorTraceKeys.UpdateChatId, $"{chat.Id}");
        }

        var user = update.GetUserOrDefault();
        if (user is not null)
        {
            tracer.AddTag(NavigatorTraceKeys.UpdateUserId, $"{user.Id}");
        }

        tracer.AddTag(NavigatorTraceKeys.UpdateType, update.Type.ToString());
    }
}