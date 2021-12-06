using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Navigator.Actions;
using Navigator.Context;
using Navigator.Providers.Telegram;
using Telegram.Bot;

namespace Navigator.Samples.Echo.Actions
{
    public class EchoActionHandler : ActionHandler<EchoAction>
    {
        public EchoActionHandler(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
        {
        }

        public override async Task<Status> Handle(EchoAction action, CancellationToken cancellationToken)
        {
            await this.GetTelegramClient().SendTextMessageAsync(this.GetTelegramChat().Id, 
                action.MessageToEcho, cancellationToken: cancellationToken);

            return Success();
        }
    }
}