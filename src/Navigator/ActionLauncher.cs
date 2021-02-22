using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Navigator.Actions;
using Navigator.Configuration;
using Navigator.Context;

namespace Navigator
{
    public class ActionLauncher : IActionLauncher
    {
        /// <summary>
        /// Navigator context accessor.
        /// </summary>
        protected readonly INavigatorContextAccessor NavigatorContextAccessor;
        
        /// <summary>
        /// Collection of available actions.
        /// </summary>
        protected readonly IEnumerable<IAction> Actions;
        
        /// <summary>
        /// Navigator otpions.
        /// </summary>
        protected readonly NavigatorOptions NavigatorOptions;

        /// <summary>
        /// Builds a new <see cref="ActionLauncher"/>.
        /// </summary>
        /// <param name="actions"></param>
        /// <param name="navigatorOptions"></param>
        /// <param name="navigatorContextAccessor"></param>
        public ActionLauncher(IEnumerable<IAction> actions, NavigatorOptions navigatorOptions,
            INavigatorContextAccessor navigatorContextAccessor)
        {
            Actions = actions;
            NavigatorOptions = navigatorOptions;
            NavigatorContextAccessor = navigatorContextAccessor;
        }

        public Task Launch()
        {
            throw new NotImplementedException();
        }

        protected IEnumerable<IAction> GetActions()
        {
            if (string.IsNullOrWhiteSpace(NavigatorContextAccessor.NavigatorContext.ActionType))
            {
                return Array.Empty<IAction>();
            }

            if (NavigatorOptions.MultipleActionsUsageIsEnabled())
            {
                return Actions
                    .Where(a => a.Type == NavigatorContextAccessor.NavigatorContext.ActionType)
                    .Where(a => a.Init(NavigatorContextAccessor.NavigatorContext)
                        .CanHandle(NavigatorContextAccessor.NavigatorContext))
                    .OrderBy(a => a.Priority).AsEnumerable();
            }

            var action = Actions
                .Where(a => a.Type == NavigatorContextAccessor.NavigatorContext.ActionType)
                .OrderBy(a => a.Priority)
                .FirstOrDefault(a => a.Init(NavigatorContextAccessor.NavigatorContext)
                    .CanHandle(NavigatorContextAccessor.NavigatorContext));

            return action is not null ? new[] {action} : Array.Empty<IAction>();
        }
    }
}