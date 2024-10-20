using Microsoft.Extensions.Logging;
using Navigator.Abstractions.Actions;
using Navigator.Abstractions.Actions.Arguments;
using Telegram.Bot.Types;

namespace Navigator.Actions.Arguments;

/// <inheritdoc />
public class ActionArgumentProvider : IActionArgumentProvider
{
    private readonly ILogger<ActionArgumentProvider> _logger;
    private readonly IEnumerable<IArgumentResolver> _resolvers;
    private readonly IServiceProvider _serviceProvider;

    /// <summary>
    ///     Initializes a new instance of the <see cref="ActionArgumentProvider" /> class.
    /// </summary>
    public ActionArgumentProvider(ILogger<ActionArgumentProvider> logger, IEnumerable<IArgumentResolver> resolvers,
        IServiceProvider serviceProvider)
    {
        _logger = logger;
        _resolvers = resolvers;
        _serviceProvider = serviceProvider;
    }

    /// <inheritdoc />
    public async ValueTask<object?[]> GetConditionArguments(Update update, BotAction action)
    {
        _logger.LogDebug("Resolving condition arguments for action {ActionName} in update {UpdateId}", action.Information.Name,
            update.Id);

        var numberOfInputs = action.Information.ConditionInputTypes.Length;
        object?[] arguments = new object[numberOfInputs];

        for (var i = 0; i < numberOfInputs; i++)
        {
            var inputType = action.Information.ConditionInputTypes[i];
            arguments[i] = await GetArgument(inputType, update, action);
        }

        _logger.LogDebug("Resolved condition arguments for action {ActionName} in update {UpdateId}: {@Arguments}",
            action.Information.Name, update.Id, arguments);

        return arguments;
    }

    /// <inheritdoc />
    public async ValueTask<object?[]> GetHandlerArguments(Update update, BotAction action)
    {
        _logger.LogDebug("Resolving handler arguments for action {ActionName} in update {UpdateId}", action.Information.Name,
            update.Id);

        var numberOfInputs = action.Information.HandlerInputTypes.Length;
        object?[] arguments = new object[numberOfInputs];

        for (var i = 0; i < numberOfInputs; i++)
        {
            var inputType = action.Information.HandlerInputTypes[i];
            arguments[i] = await GetArgument(inputType, update, action);
        }

        _logger.LogDebug("Resolved handler arguments for action {ActionName} in update {UpdateId}: {@Arguments}",
            action.Information.Name, update.Id, arguments);

        return arguments;
    }

    private async ValueTask<object?> GetArgument(Type inputType, Update update, BotAction action)
    {
        _logger.LogDebug("Resolving argument of type {InputType} for action {ActionName} in update {UpdateId}", inputType,
            action.Information.Name, update.Id);

        var argument = default(object?);

        foreach (var resolver in _resolvers)
        {
            argument = await resolver.GetArgument(inputType, update, action);

            if (argument is not null) break;
        }

        return argument ?? _serviceProvider.GetService(inputType);
    }
}