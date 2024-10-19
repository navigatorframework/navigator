using Microsoft.Extensions.Logging;
using Navigator.Abstractions.Catalog;
using Navigator.Abstractions.Classifier;
using Navigator.Abstractions.Pipelines.Context;
using Navigator.Abstractions.Pipelines.Steps;
using Navigator.Catalog.Factory;

namespace Navigator.Pipelines.Steps;

/// <summary>
///     Default implementation of <see cref="IActionResolutionPipelineStep" />.
/// </summary>
public class DefaultActionResolutionStep : IActionResolutionPipelineStep
{
    private readonly IBotActionCatalog _catalog;
    private readonly IUpdateClassifier _classifier;
    private readonly ILogger<DefaultActionResolutionStep> _logger;

    /// <summary>
    ///     Initializes a new instance of the <see cref="DefaultActionResolutionStep" /> class.
    /// </summary>
    public DefaultActionResolutionStep(BotActionCatalogFactory catalogFactory, ILogger<DefaultActionResolutionStep> logger,
        IUpdateClassifier classifier)
    {
        _catalog = catalogFactory.Retrieve();
        _logger = logger;
        _classifier = classifier;
    }

    /// <inheritdoc />
    public async Task InvokeAsync(NavigatorActionResolutionContext context, PipelineStepHandlerDelegate next)
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