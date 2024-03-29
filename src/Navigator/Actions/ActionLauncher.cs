using System.Collections.Immutable;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Navigator.Configuration;
using Navigator.Context.Accessor;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;

namespace Navigator.Actions;

internal class ActionLauncher : IActionLauncher
{
    private readonly ILogger<ActionLauncher> _logger;
    private readonly INavigatorContextAccessor _navigatorContextAccessor;
    private readonly IServiceProvider _serviceProvider;
    private readonly ISender _sender;
    private readonly NavigatorOptions _navigatorOptions;
    private readonly ImmutableDictionary<string, Type[]> _actions;
    private readonly ImmutableDictionary<string, ushort> _priorities;

    public ActionLauncher(ILogger<ActionLauncher> logger, NavigatorOptions navigatorOptions, INavigatorContextAccessor navigatorContextAccessor, IServiceProvider serviceProvider, ISender sender)
    {
        _logger = logger;
        _navigatorContextAccessor = navigatorContextAccessor;
        _serviceProvider = serviceProvider;
        _sender = sender;
        _navigatorOptions = navigatorOptions;
        _actions = _navigatorOptions.RetrieveActions();
        _priorities = _navigatorOptions.RetrievePriorities();
    }

    public async Task Launch()
    {
        var actions = GetActions();
        var chatId = _navigatorContextAccessor.NavigatorContext.Conversation.Chat!.Id;

        foreach (var action in actions)
        {
            try
            {
                if (_navigatorOptions.TypingNotificationIsEnabled())
                {
                    await _navigatorContextAccessor.NavigatorContext.Client.SendChatActionAsync(chatId, ChatAction.Typing);
                }
                
                await _sender.Send(action);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Unhandled exception running {ActionName} ({@ActionData})", action.GetType().Name, action);
            }
        }
    }

    private IEnumerable<IAction> GetActions()
    {
        if (string.IsNullOrWhiteSpace(_navigatorContextAccessor.NavigatorContext.ActionType))
        {
            return Array.Empty<IAction>();
        }

        var actions = _actions
            .Where(a => 
                a.Key == _navigatorContextAccessor.NavigatorContext.ActionType ||
                a.Key == nameof(ProviderAgnosticAction))
            .ToImmutableList();
            
        if (_navigatorOptions.MultipleActionsUsageIsEnabled())
        {
            return actions
                .SelectMany(groups => groups.Value)
                .SelectMany(actionType => _serviceProvider.GetServices(actionType)
                    .Select(action =>((IAction) action!, actionType.FullName)))
                .Where(a => a.Item1.CanHandleCurrentContext())
                .OrderBy(a => _priorities.GetValueOrDefault(a.FullName ?? string.Empty, Priority.Default))
                .Select(a => a.Item1)
                .AsEnumerable();
        }
            
        var action = actions
            .SelectMany(groups => groups.Value)
            .SelectMany(actionType => _serviceProvider.GetServices(actionType)
                .Select(action =>((IAction) action!, actionType.FullName)))
            .OrderBy(a => _priorities.GetValueOrDefault(a.FullName ?? string.Empty, Priority.Default))
            .Select(a => a.Item1)
            .FirstOrDefault(a => a.CanHandleCurrentContext());

        return action is not null ? new[] {action} : Array.Empty<IAction>();
    }
}