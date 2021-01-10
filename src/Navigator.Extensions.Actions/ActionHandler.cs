using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Navigator.Abstractions;

namespace Navigator.Extensions.Actions
{
    public abstract class ActionHandler<TAction> : IRequestHandler<TAction> where TAction : IAction
    {
        public readonly INavigatorContext Ctx;

        public ActionHandler(INavigatorContext ctx)
        {
            Ctx = ctx;
        }

        public abstract Task<Unit> Handle(TAction request, CancellationToken cancellationToken);
    }
}