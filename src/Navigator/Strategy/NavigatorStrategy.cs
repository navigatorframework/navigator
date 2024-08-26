using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Navigator.Actions;
using Navigator.Catalog;
using Navigator.Catalog.Factory;
using Navigator.Client;
using Navigator.Configuration.Options;
using Navigator.Strategy.Classifier;
using Navigator.Strategy.TypeProvider;
using Navigator.Telegram;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Navigator.Strategy;

/// <summary>
///     Implements a strategy for dynamic decision-making based on incoming updates. This strategy involves classifying updates,
///     selecting relevant actions from a catalog, and executing those actions asynchronously.
///     <seealso cref="BotActionCatalog" />
///     <seealso cref="IUpdateClassifier" />
/// </summary>
public class NavigatorStrategy : INavigatorStrategy
{
    private readonly IMemoryCache _cache;
    private readonly BotActionCatalog _catalog;
    private readonly IUpdateClassifier _classifier;
    private readonly INavigatorClient _client;
    private readonly ILogger<INavigatorStrategy> _logger;
    private readonly INavigatorOptions _options;
    private readonly IServiceProvider _serviceProvider;

    /// <summary>
    ///     Initializes a new instance of the <see cref="NavigatorStrategy" /> class.
    /// </summary>
    /// <param name="cache">The <see cref="IMemoryCache" /> instance.</param>
    /// <param name="catalogFactory">The <see cref="BotActionCatalog" /> instance.</param>
    /// <param name="classifier">The <see cref="IUpdateClassifier" /> instance.</param>
    /// <param name="client">The <see cref="INavigatorClient" /> instance.</param>
    /// <param name="options">The <see cref="INavigatorOptions" /> instance.</param>
    /// <param name="serviceProvider">The <see cref="IServiceProvider" /> instance.</param>
    /// <param name="logger">The <see cref="ILogger{TCategoryName}" /> instance.</param>
    public NavigatorStrategy(IMemoryCache cache, BotActionCatalogFactory catalogFactory, IUpdateClassifier classifier,
        INavigatorClient client, INavigatorOptions options, IServiceProvider serviceProvider, ILogger<INavigatorStrategy> logger)
    {
        _cache = cache;
        _catalog = catalogFactory.Retrieve();
        _classifier = classifier;
        _options = options;
        _serviceProvider = serviceProvider;
        _logger = logger;
        _client = client;
    }

    /// <summary>
    ///     Processes an <see cref="Update" /> by determining the appropriate action type, retrieving relevant <see cref="BotAction" /> from
    ///     the<see cref="BotActionCatalog" />, filtering those actions based on the <see cref="Update" />, executing each filtered action
    ///     asynchronously.
    /// </summary>
    /// <param name="update">The <see cref="Update" /> object to be processed.</param>
    public async Task Invoke(Update update)
    {
        _logger.LogInformation("Processing update {UpdateId}", update.Id);

        var chat = update.GetChatOrDefault();

        // if (_options.ChatActionNotificationIsEnabled() && chat is not null)
        // {
        //     _logger.LogInformation("Sending typing notification to chat {ChatId}", chat.Id);
        //
        //     await _client.SendChatActionAsync(chat, ChatAction.Typing);
        // }

        var updateCategory = await _classifier.Process(update);

        _logger.LogInformation("Update {UpdateId} classified as {UpdateCategory}", update.Id,
            updateCategory);

        var relevantActions = _catalog.Retrieve(updateCategory);

        _logger.LogInformation("Found {RelevantActionsCount} relevant actions for {UpdateId}", relevantActions.Count(), update.Id);

        _logger.LogDebug("Actions relevant for update {UpdateId}: {ActionsFound}", update.Id,
            string.Join(", ", relevantActions.Select(action => action.Information.Name)));

        relevantActions = relevantActions.Where(action => IsNotInCooldown(action, update));

        _logger.LogDebug("Actions relevant for update {UpdateId} which are not in cooldown: {ActionsFound}", update.Id,
            string.Join(", ", relevantActions.Select(action => action.Information.Name)));

        await foreach (var action in FilterActionsThatCanHandleUpdate(relevantActions, update))
        {
            _logger.LogInformation("Executing action {ActionName}", action.Information.Name);

            if (_options.ChatActionNotificationIsEnabled() && chat is not null && action.Information.ChatAction is not null)
            {
                _logger.LogDebug("Sending {ChatAction} notification to chat {ChatId}", action.Information.ChatAction.Value, chat.Id);

                await _client.SendChatActionAsync(chat, action.Information.ChatAction.Value);
            }

            try
            {
                await ExecuteAction(action, update);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to execute action {ActionName} for update {UpdateId}", action.Information.Name, update.Id);
            }
        }
    }

    /// <summary>
    ///     Checks if the given <see cref="BotAction" /> is in cooldown.
    /// </summary>
    /// <param name="botAction">The <see cref="BotAction" /> object to be checked.</param>
    /// <param name="update">The <see cref="Update" /> object that triggered the execution of the <see cref="BotAction" />.</param>
    /// <returns><c>true</c> if the <see cref="BotAction" /> is in cooldown; otherwise, <c>false</c>.</returns>
    private bool IsNotInCooldown(BotAction botAction, Update update)
    {
        var isNotInCooldown = !_cache.TryGetValue(GenerateCacheKey(botAction, update), out _);

        if (isNotInCooldown is false) _logger.LogDebug("Discarding action {ActionName} because is in cooldown", botAction.Information.Name);

        return isNotInCooldown;
    }

    /// <summary>
    ///     Generates a cache key based on the <see cref="BotAction" /> and <see cref="Update" />. It ultimately uses the chat id, if available.
    /// </summary>
    /// <param name="botAction">The <see cref="BotAction" /> object to be used in the cache key.</param>
    /// <param name="update">The <see cref="Update" /> object to be used in the cache key.</param>
    /// <returns>A <see cref="string" /> representing the cache key.</returns>
    private static string GenerateCacheKey(BotAction botAction, Update update)
    {
        return $"{botAction.Id}:{update.GetChatOrDefault()?.Id}";
    }

    /// <summary>
    ///     Filters the given collection of <see cref="BotAction" /> by executing the condition of each action.
    ///     If the condition returns true, the action is included in the resulting collection and returned to the caller.
    /// </summary>
    /// <param name="actions">The collection of <see cref="BotAction" /> to be filtered.</param>
    /// <param name="update">The <see cref="Update" /> object that triggered the execution of the strategy.</param>
    /// <returns>An <see cref="IAsyncEnumerable{T}" /> of <see cref="BotAction" /> that passes the condition check.</returns>
    private async IAsyncEnumerable<BotAction> FilterActionsThatCanHandleUpdate(IEnumerable<BotAction> actions, Update update)
    {
        foreach (var action in actions)
        {
            var numberOfInputs = action.Information.ConditionInputTypes.Length;
            var arguments = new object?[numberOfInputs];

            for (var i = 0; i < numberOfInputs; i++)
            {
                var inputType = action.Information.ConditionInputTypes[i];
                arguments[i] = await GetArgument(inputType, update, action);
            }

            if (!await action.ExecuteCondition(arguments))
            {
                _logger.LogDebug("Discarding action {ActionName} because condition is not met", action.Information.Name);

                continue;
            }

            yield return action;

            if (_options.MultipleActionsUsageIsEnabled() is false)
            {
                _logger.LogDebug("Discarding other actions because multiple actions usage is disabled");

                break;
            }
        }
    }

    /// <summary>
    ///     Executes the <see cref="BotAction.ExecuteHandler" /> method of a <see cref="BotAction" /> after
    ///     retrieving the necessary input arguments. Arguments are retrieved by calling <see cref="GetArgument" /> for each
    ///     input type specified in the <see cref="BotActionInformation" />.
    /// </summary>
    /// <param name="action">The <see cref="BotAction" /> to be executed.</param>
    /// <param name="update">The <see cref="Update" /> object that triggered the execution of the <see cref="BotAction" />.</param>
    private async Task ExecuteAction(BotAction action, Update update)
    {
        if (action.Information.Chances is not null && Random.Shared.NextDouble() > action.Information.Chances)
        {
            _logger.LogDebug("Discarding action {ActionName} because of configured chances ({Chances})", action.Information.Name,
                action.Information.Chances);

            return;
        }

        var numberOfInputs = action.Information.HandlerInputTypes.Length;
        object?[] arguments = new object[numberOfInputs];

        for (var i = 0; i < numberOfInputs; i++)
        {
            var inputType = action.Information.HandlerInputTypes[i];
            arguments[i] = await GetArgument(inputType, update, action);
        }

        await action.ExecuteHandler(arguments);

        if (action.Information.Cooldown.HasValue)
        {
            _logger.LogDebug("Setting action {ActionName} to cooldown for {Cooldown} minutes", action.Information.Name,
                action.Information.Cooldown.Value.TotalMinutes);

            _cache.Set(GenerateCacheKey(action, update), true, action.Information.Cooldown.Value);
        }
    }

    private async Task<object?> GetArgument(Type inputType, Update update, BotAction action)
    {
        var argument = default(object?);

        var providers = _serviceProvider.GetServices<IArgumentTypeProvider>();

        foreach (var provider in providers)
        {
            argument = await provider.GetArgument(inputType, update, action);

            if (argument is not null) break;
        }

        return argument ?? _serviceProvider.GetRequiredService(inputType);
    }

    private async ValueTask<object?> GetArgumentUsingProviders(Type inputType, Update update, BotAction action)
    {
        var providers = _serviceProvider.GetServices<IArgumentTypeProvider>();

        foreach (var provider in providers) return await provider.GetArgument(inputType, update, action);

        return default;
    }
}