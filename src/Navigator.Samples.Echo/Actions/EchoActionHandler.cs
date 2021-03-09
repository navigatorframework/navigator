using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Navigator.Actions;
using Navigator.Actions.Model;
using Navigator.Context;
using Navigator.Providers.Telegram;

namespace Navigator.Samples.Echo.Actions
{
    public class EchoActionHandler : ActionHandler<EchoAction>
    {
        public EchoActionHandler(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
        {
        }

        public override async Task<ActionStatus> Handle(EchoAction action, CancellationToken cancellationToken)
        {
            await NavigatorContext.Provider.GetTelegramClient().SendTextMessageAsync(NavigatorContext.GetTelegramChat(), 
                action.MessageToEcho, cancellationToken: cancellationToken);

            return Success();
        }
    }
}