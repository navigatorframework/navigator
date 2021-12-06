using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Navigator.Configuration;
using Navigator.Context;

namespace Navigator.Actions
{
    internal class ActionLauncher : IActionLauncher
    {
        private readonly ILogger<ActionLauncher> _logger;
        private readonly NavigatorOptions _navigatorOptions;
        private readonly INavigatorContextAccessor _navigatorContextAccessor;
        private readonly IServiceProvider _serviceProvider;
        private readonly ISender _sender;

        public ActionLauncher(ILogger<ActionLauncher> logger, NavigatorOptions navigatorOptions, INavigatorContextAccessor navigatorContextAccessor, IServiceProvider serviceProvider, ISender sender)
        {
            _logger = logger;
            _navigatorOptions = navigatorOptions;
            _navigatorContextAccessor = navigatorContextAccessor;
            _serviceProvider = serviceProvider;
            _sender = sender;
        }

        public async Task Launch()
        {
            var actions = GetActions();
            
            foreach (var action in actions)
            {
                try
                {
                    await _sender.Send(action);
                }
                catch (Exception e)
                {
                    _logger.LogError(e, "Unhandled exception running {@Action}", action);
                }
            }
        }

        private IEnumerable<IAction> GetActions()
        {
            if (string.IsNullOrWhiteSpace(_navigatorContextAccessor.NavigatorContext.ActionType))
            {
                return Array.Empty<IAction>();
            }

            var _actions = _navigatorOptions.RetrieveActions()
                    .Where(a => a.Key == _navigatorContextAccessor.NavigatorContext.ActionType);
            
            if (_navigatorOptions.MultipleActionsUsageIsEnabled())
            {
                return _actions
                    .Select(pair => (IAction) _serviceProvider.GetService(pair.Value)!)
                    .Where(a => a.CanHandleCurrentContext())
                    .OrderBy(a => a.Priority)
                    .AsEnumerable();
            }

            var action = _actions
                .Select(pair => (IAction) _serviceProvider.GetService(pair.Value)!)
                .OrderBy(a => a.Priority)
                .FirstOrDefault(a => a.CanHandleCurrentContext());

            return action is not null ? new[] {action} : Array.Empty<IAction>();
        }
    }
}