using Microsoft.Extensions.DependencyInjection;
using Navigator.Actions;
using Navigator.Catalog;
using Navigator.Client;
using Navigator.Entities;
using Navigator.Strategy.Classifier;
using Navigator.Telegram;
using Telegram.Bot.Types;

namespace Navigator.Strategy;

public class NavigatorStrategy : INavigatorStrategy
{
    private readonly BotActionCatalog _catalog;
    private readonly IUpdateClassifier _classifier;
    private readonly IServiceProvider _serviceProvider;

    public NavigatorStrategy(IBotActionCatalogFactory catalogFactory, IUpdateClassifier classifier,
        IServiceProvider serviceProvider)
    {
        _catalog = catalogFactory.Retrieve();
        _classifier = classifier;
        _serviceProvider = serviceProvider;
    }

    public async Task Invoke(Update update)
    {
        var actionType = await _classifier.Process(update);

        var relevantActions = _catalog.Retrieve(actionType);

        foreach (var action in await FilterActionsThatCanHandleUpdate(relevantActions, update))
        {
            await ExecuteAction(action, update);
        }
    }

    //TODO: rework this into IAsyncEnumerable and yield
    private async Task<IEnumerable<BotAction>> FilterActionsThatCanHandleUpdate(IEnumerable<BotAction> actions, Update update)
    {
        var successActions = new List<BotAction>();

        foreach (var action in actions)
        {
            var arguments = new List<object>();

            foreach (var inputType in action.Information.ConditiionInputTypes)
            {
                arguments.Add(inputType switch
                {
                    not null when inputType == typeof(Update)
                        => update,
                    not null when inputType == typeof(Conversation) 
                        => update.GetConversation(),
                    not null when inputType == typeof(Bot) 
                        => await _serviceProvider.GetRequiredService<INavigatorClient>().GetProfile(),
                    not null => _serviceProvider.GetRequiredService(inputType),
                    //TODO: this exception should never happen.
                    _ => throw new NavigatorException()
                });
            }

            if (await action.ExecuteCondition(arguments))
            {
                successActions.Add(action);
            }
        }

        return successActions;
    }

    private async Task ExecuteAction(BotAction action, Update update)
    {
        var arguments = new List<object>();

        foreach (var inputType in action.Information.HandlerInputTypes)
        {
            arguments.Add(inputType switch
            {
                not null when inputType == typeof(INavigatorContext) 
                    => _serviceProvider.GetRequiredService<INavigatorContextAccessor>().NavigatorContext,
                not null when inputType == typeof(INavigatorClient) 
                    => _serviceProvider.GetRequiredService<INavigatorContextAccessor>().NavigatorContext.Client,
                not null when inputType == typeof(Update)
                    => update,
                not null => _serviceProvider.GetRequiredService(inputType),
                //TODO: this exception should never happen.
                _ => throw new NavigatorException()
            });
        }

        await action.ExecuteHandler(arguments);
    }
}