using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Navigator.Actions;
using Navigator.Context;

namespace Navigator
{
    public class ActionLauncher : IActionLauncher
    {
        protected readonly INavigatorContext NavigatorContext;
        protected readonly IEnumerable<IAction> Actions;

        public Task Launch()
        {
            
        }
        
        protected IEnumerable<IAction> GetActions()
        {
            var actions = new List<IAction>();
            var actionType = NavigatorContext.;

            if (string.IsNullOrWhiteSpace(actionType))
            {
                return actions;
            }

            if (MultipleActionsUsage)
            {
                actions = Actions
                    .Where(a => a.Type == actionType)
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