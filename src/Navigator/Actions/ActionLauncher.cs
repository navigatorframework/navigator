using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Navigator.Actions.Model;
using Navigator.Configuration;
using Navigator.Context;

namespace Navigator.Actions
{
    internal class ActionLauncher : IActionLauncher
    {
        private readonly INavigatorContextAccessor _navigatorContextAccessor;

        private readonly IEnumerable<IAction> _actions;

        private readonly NavigatorOptions _navigatorOptions;

        private readonly ISender _sender;
        
        public ActionLauncher(IEnumerable<IAction> actions, NavigatorOptions navigatorOptions,
            INavigatorContextAccessor navigatorContextAccessor, ISender sender)
        {
            _actions = actions;
            _navigatorOptions = navigatorOptions;
            _navigatorContextAccessor = navigatorContextAccessor;
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
                    //TODO: logs
                }
            }
        }

        private IEnumerable<IAction> GetActions()
        {
            if (string.IsNullOrWhiteSpace(_navigatorContextAccessor.NavigatorContext.ActionType))
            {
                return Array.Empty<IAction>();
            }

            if (_navigatorOptions.MultipleActionsUsageIsEnabled())
            {
                return _actions
                    .Where(a => a.Type == _navigatorContextAccessor.NavigatorContext.ActionType)
                    .Where(a => a.Init(_navigatorContextAccessor.NavigatorContext)
                        .CanHandle(_navigatorContextAccessor.NavigatorContext))
                    .OrderBy(a => a.Priority).AsEnumerable();
            }

            var action = _actions
                .Where(a => a.Type == _navigatorContextAccessor.NavigatorContext.ActionType)
                .OrderBy(a => a.Priority)
                .FirstOrDefault(a => a.Init(_navigatorContextAccessor.NavigatorContext)
                    .CanHandle(_navigatorContextAccessor.NavigatorContext));

            return action is not null ? new[] {action} : Array.Empty<IAction>();
        }
    }
}