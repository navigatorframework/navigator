using Microsoft.Extensions.Logging;
using Navigator.Catalog;
using Navigator.Catalog.Factory;
using Navigator.Strategy.Classifier;
using Navigator.Strategy.Context;
using Navigator.Strategy.Pipelines.Abstractions;

namespace Navigator.Strategy.Pipelines.Bundled.Steps;

/// <summary>
///     Default implementation of <see cref="IActionResolutionPipelineStep" />.
/// </summary>
public class DefaultActionResolutionStep : IActionResolutionPipelineStep
{
    private readonly BotActionCatalog _catalog;
    private readonly IUpdateClassifier _classifier;
    private readonly ILogger<DefaultActionResolutionStep> _logger;

    /// <summary>
    ///     Initializes a new instance of the <see cref="DefaultActionResolutionStep" /> class.
    /// </summary>
    /// <param name="catalogFactory">A <see cref="BotActionCatalogFactory" />.</param>
    /// <param name="logger">An instance of <see cref="ILogger" />.</param>
    /// <param name="classifier">An instance of <see cref="IUpdateClassifier" />.</param>
    public DefaultActionResolutionStep(BotActionCatalogFactory catalogFactory, ILogger<DefaultActionResolutionStep> logger,
        IUpdateClassifier classifier)
    {
        _catalog = catalogFactory.Retrieve();
        _logger = logger;
        _classifier = classifier;
    }

    /// <inheritdoc />
    public async Task InvokeAsync(NavigatorStrategyContext context, PipelineStepHandlerDelegate next)
    {
        _logger.LogInformation("Resolving actions for update {UpdateId}", context.Update.Id);

        _logger.LogDebug("Classifying update {UpdateId}", context.Update.Id);

        context.UpdateCategory = await _classifier.Process(context.Update);

        _logger.LogInformation("Update {UpdateId} classified as {UpdateCategory}", context.Update.Id, context.UpdateCategory);

        var relevantActions = _catalog.Retrieve(context.UpdateCategory).ToArray();

        _logger.LogInformation("Found {RelevantActionsCount} relevant actions for update {UpdateId}", relevantActions.Count(),
            context.Update.Id);

        _logger.LogDebug("Actions relevant for update {UpdateId}: {ActionsFound}", context.Update.Id,
            string.Join(", ", relevantActions.Select(action => action.Information.Name)));

        context.Actions.AddRange(relevantActions);

        _logger.LogInformation("Finished resolving actions for update {UpdateId}", context.Update.Id);

        await next();
    }
}