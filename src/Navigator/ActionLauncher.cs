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
        protected readonly INavigatorContext NavigatorContext;
        protected readonly IEnumerable<IAction> Actions;
        protected readonly NavigatorOptions NavigatorOptions;

        public ActionLauncher(INavigatorContext navigatorContext, IEnumerable<IAction> actions, NavigatorOptions navigatorOptions)
        {
            NavigatorContext = navigatorContext;
            Actions = actions;
            NavigatorOptions = navigatorOptions;
        }

        public Task Launch()
        {
            
        }
        
        protected IEnumerable<IAction> GetActions()
        {
            var actions = new List<IAction>();
            var actionType = NavigatorContext.ActionType;

            if (string.IsNullOrWhiteSpace(actionType))
            {
                return actions;
            }

            if (NavigatorOptions.MultipleActionsUsageIsEnabled())
            {
                actions = Actions
                    .Where(a => a. == actionType)
                    .Where(a => a.Init(Ctx).CanHandle(Ctx))
                    .OrderBy(a => a.Order)
                    .ToList();
            }
            else
            {
                var action = Actions
                    .Where(a => a.Type == actionType)
                    .OrderBy(a => a.Order)
                    .FirstOrDefault(a => a.Init(Ctx).CanHandle(Ctx));

                if (action != null)
                {
                    actions.Add(action);
                }
            }

            return actions;
        }
    }
}