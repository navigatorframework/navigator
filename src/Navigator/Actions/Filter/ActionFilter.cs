using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Navigator.Actions.Model;

namespace Navigator.Actions.Filter
{
    public abstract class ActionFilter<TAction> : IPipelineBehavior<TAction, ActionStatus> where TAction : IAction
    {
        public abstract Task<ActionStatus> Handle(TAction action, CancellationToken cancellationToken, RequestHandlerDelegate<ActionStatus> next);
    }
}