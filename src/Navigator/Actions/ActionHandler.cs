using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Navigator.Actions.Model;
using Navigator.Context;
using Navigator.Replies;

namespace Navigator.Actions
{
    public abstract class ActionHandler<TAction> : IRequestHandler<TAction, ActionResult> where TAction : IAction
    {
        public INavigatorContext NavigatorContext;

        protected ActionHandler(INavigatorContextAccessor navigatorContextAccessor)
        {
            NavigatorContext = navigatorContextAccessor.NavigatorContext;
        }

        public abstract Task<ActionResult> Handle(TAction request, CancellationToken cancellationToken);

        public Task Reply(Action<ReplyBuilderOptions> replyBuilder, CancellationToken cancellationToken)
        {
            
            
            NavigatorContext.Provider.HandleReply()
        }
    }
}