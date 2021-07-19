using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Navigator.Actions.Filters
{
    public abstract class ActionFilter<TAction> : IPipelineBehavior<TAction, Status> where TAction : IAction
    {
        public abstract Task<Status> Handle(TAction action, CancellationToken cancellationToken, RequestHandlerDelegate<Status> next);
    }
}