using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Navigator.Actions;
using Navigator.Bundled.Extensions.Update;
using Navigator.Catalog;
using Navigator.Context;
using Navigator.Context.Accessor;
using Navigator.Strategy.Classifier;
using Telegram.Bot.Types;

namespace Navigator.Strategy;

public class NavigatorStrategy : INavigatorStrategy
{
    private readonly ILogger<NavigatorStrategy> _logger;
    private readonly BotActionCatalog _catalog;
    private readonly IUpdateClassifier _classifier;
    private readonly IServiceProvider _serviceProvider;
    private readonly INavigatorContextAccessor _contextAccessor;

    public NavigatorStrategy(ILogger<NavigatorStrategy> logger, IBotActionCatalogFactory catalogFactory, IUpdateClassifier classifier,
        IServiceProvider serviceProvider, INavigatorContextAccessor contextAccessor)
    {
        _logger = logger;
        _catalog = catalogFactory.Retrieve();
        _classifier = classifier;
        _serviceProvider = serviceProvider;
        _contextAccessor = contextAccessor;
    }

    public async Task Invoke(Update update)
    {
        var actionType = await _classifier.Process(update);

        var relevantActions = _catalog.Retrieve(actionType);

        foreach (var action in await FilterActionsThatCanHandleUpdate(relevantActions))
        {
            await ExecuteAction(action);
        }
    }

    //TODO: rework this into IAsyncEnumerable and yield
    private async Task<IEnumerable<BotAction>> FilterActionsThatCanHandleUpdate(IEnumerable<BotAction> actions)
    {
        var successActions = new List<BotAction>();

        foreach (var action in actions)
        {
            var arguments = new List<object>();

            foreach (var inputType in action.Information.ConditiionInputTypes)
            {
                arguments.Add(inputType switch
                {
                    not null when inputType == typeof(INavigatorContext) => _contextAccessor.NavigatorContext,
                    not null when inputType == typeof(Update) => _contextAccessor.NavigatorContext.GetUpdate(),
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

    private async Task ExecuteAction(BotAction action)
    {
        var arguments = new List<object>();

        foreach (var inputType in action.Information.HandlerInputTypes)
        {
            arguments.Add(inputType switch
            {
                not null when inputType == typeof(INavigatorContext) => _contextAccessor.NavigatorContext,
                not null when inputType == typeof(Update) => _contextAccessor.NavigatorContext.GetUpdate(),
                not null => _serviceProvider.GetRequiredService(inputType),
                //TODO: this exception should never happen.
                _ => throw new NavigatorException()
            });
        }

        await action.ExecuteHandler(arguments);
    }
}