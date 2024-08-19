using Microsoft.Extensions.DependencyInjection;
using Navigator.Actions;
using Navigator.Catalog;
using Navigator.Client;
using Navigator.Entities;
using Navigator.Strategy.Classifier;
using Navigator.Telegram;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Chat = Navigator.Entities.Chat;

namespace Navigator.Strategy;

/// <summary>
/// Implements a strategy for dynamic decision-making based on incoming updates. This strategy involves classifying updates,
/// selecting relevant actions from a catalog, and executing those actions asynchronously.
/// <seealso cref="BotActionCatalog"/>
/// <seealso cref="IUpdateClassifier"/>
/// </summary>
public class NavigatorStrategy : INavigatorStrategy
{
    private readonly BotActionCatalog _catalog;
    private readonly IUpdateClassifier _classifier;
    private readonly IServiceProvider _serviceProvider;

    /// <summary>
    /// Initializes a new instance of <see cref="NavigatorStrategy"/>.
    /// </summary>
    /// <param name="catalogFactory">An instance of <see cref="IBotActionCatalogFactory"/>.</param>
    /// <param name="classifier">An instance of <see cref="IUpdateClassifier"/>.</param>
    /// <param name="serviceProvider">An instance of <see cref="IServiceProvider"/></param>.
    public NavigatorStrategy(IBotActionCatalogFactory catalogFactory, IUpdateClassifier classifier,
        IServiceProvider serviceProvider)
    {
        _catalog = catalogFactory.Retrieve();
        _classifier = classifier;
        _serviceProvider = serviceProvider;
    }

    /// <summary>
    /// Processes an <see cref="Update"/> by determining the appropriate action type,
    /// retrieving relevant <see cref="BotAction"/> from the <see cref="BotActionCatalog"/>,
    /// filtering those actions based on the <see cref="Update"/>,
    /// and executing each filtered action asynchronously.
    /// </summary>
    /// <param name="update">The <see cref="Update"/> object to be processed.</param>
    public async Task Invoke(Update update)
    {
        var actionType = await _classifier.Process(update);

        var relevantActions = _catalog.Retrieve(actionType);

        await foreach (var action in FilterActionsThatCanHandleUpdate(relevantActions, update))
        {
            await ExecuteAction(action, update);
        }
    }

    private async IAsyncEnumerable<BotAction> FilterActionsThatCanHandleUpdate(IEnumerable<BotAction> actions, Update update)
    {
        foreach (var action in actions)
        {
            var arguments = new List<object>();

            foreach (var inputType in action.Information.ConditionInputTypes)
            {
                var argument = await GetArgument(inputType, update, action);
                arguments.Add(argument);
            }

            if (await action.ExecuteCondition(arguments))
            {
                yield return action;
            }
        }
    }

    private async Task ExecuteAction(BotAction action, Update update)
    {
        var arguments = new List<object>();

        foreach (var inputType in action.Information.HandlerInputTypes)
        {
            var argument = await GetArgument(inputType, update, action);
            arguments.Add(argument);
        }

        await action.ExecuteHandler(arguments);
    }
    
    private async Task<object> GetArgument(Type inputType, Update update, BotAction action)
    {
        var argument = inputType switch
        {
            not null when inputType == typeof(Update)
                => update,
            not null when inputType == typeof(Conversation) 
                => update.GetConversation(),
            not null when inputType == typeof(Chat) 
                => update.GetChatOrDefault()!,
            not null when inputType == typeof(Conversation) 
                => update.GetUserOrDefault()!,
            not null when inputType == typeof(Bot) 
                => await _serviceProvider.GetRequiredService<INavigatorClient>().GetProfile(),
            not null when inputType == typeof(string[]) && action.Information.Category.Subkind == nameof(MessageEntityType.BotCommand)
                => update.Message!.ExtractArguments(),
            not null => _serviceProvider.GetRequiredService(inputType),
            //TODO: this exception should never happen.
            _ => throw new NavigatorException()
        };
        return argument;
    }
}