using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Navigator.Abstractions;
using Navigator.Abstractions.Extensions;
using Navigator.Extensions.Actions;
using Navigator.Extensions.Store.Abstractions.Extensions;
using Navigator.Samples.Echo.Entity;
using Newtonsoft.Json;

namespace Navigator.Samples.Echo.Actions
{
    public class EchoActionHandler : ActionHandler<EchoAction>
    {
        public EchoActionHandler(INavigatorContext ctx) : base(ctx)
        {
        }

        public override async Task<Unit> Handle(EchoAction request, CancellationToken cancellationToken)
        {
            var user = Ctx.GetUser<SampleUser>();
            
            await Ctx.Client.SendTextMessageAsync(Ctx.Update.Message.Chat.Id, request.EchoMessage + ":" + JsonConvert.SerializeObject(user, settings: new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            }), cancellationToken: cancellationToken);
            
            return Unit.Value;
        }
    }
}