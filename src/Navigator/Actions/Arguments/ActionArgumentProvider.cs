using Microsoft.Extensions.Logging;
using Navigator.Abstractions.Actions;
using Navigator.Abstractions.Actions.Arguments;
using Telegram.Bot.Types;

namespace Navigator.Actions.Arguments;

public class ActionArgumentProvider : IActionArgumentProvider
{
    private readonly ILogger<ActionArgumentProvider> _logger;
    private readonly IArgumentResolver[] _resolvers;
    private readonly IServiceProvider _serviceProvider;

    public ActionArgumentProvider(ILogger<ActionArgumentProvider> logger, IArgumentResolver[] resolvers,
        IServiceProvider serviceProvider)
    {
        _logger = logger;
        _resolvers = resolvers;
        _serviceProvider = serviceProvider;
    }

    /// <inheritdoc />
    public async ValueTask<object?> GetArgument(Type inputType, Update update, BotAction action)
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