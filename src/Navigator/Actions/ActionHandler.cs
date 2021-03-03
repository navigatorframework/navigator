using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Navigator.Actions.Model;
using Navigator.Context;
using Navigator.Replies;

namespace Navigator.Actions
{
    public abstract class ActionHandler<TAction> : IRequestHandler<TAction, ActionStatus> where TAction : IAction
    {
        public INavigatorContext NavigatorContext;

        protected ActionHandler(INavigatorContextAccessor navigatorContextAccessor)
        {
            NavigatorContext = navigatorContextAccessor.NavigatorContext;
        }

        public abstract Task<ActionStatus> Handle(TAction request, CancellationToken cancellationToken);

        public static ActionStatus Success()
        {
            return new(true);
        }
        
        
        public static ActionStatus Error()
        {
            return new(false);
        }
    }
}