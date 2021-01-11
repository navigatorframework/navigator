using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Navigator.Abstractions;
using Navigator.Extensions.Actions;
using Navigator.Extensions.Store.Abstractions.Extensions;

namespace Navigator.Samples.DefaultStore.Actions
{
    public class EchoActionHandler : ActionHandler<EchoAction>
    {
        public EchoActionHandler(INavigatorContext ctx) : base(ctx)
        {
        }

        public override async Task<Unit> Handle(EchoAction request, CancellationToken cancellationToken)
        {
            await Ctx.Client.SendTextMessageAsync(Ctx.Update.Message.Chat.Id, request.EchoMessage, cancellationToken: cancellationToken);
            
            return Unit.Value;
        }
    }
}