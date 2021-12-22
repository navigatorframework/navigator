using System.Threading;
using System.Threading.Tasks;
using Navigator.Actions;
using Navigator.Context;
using Navigator.Extensions.Store;
using Navigator.Providers.Telegram;
using Telegram.Bot;

namespace Navigator.Samples.Store.Actions;

public class EchoActionHandler : ActionHandler<EchoAction>
{
    public EchoActionHandler(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
    {
    }

    public override async Task<Status> Handle(EchoAction action, CancellationToken cancellationToken)
    {
        var store = NavigatorContext.GetStoreOrDefault();
        
        await this.GetTelegramClient().SendTextMessageAsync(this.GetTelegramChat().Id, 
            action.MessageToEcho, cancellationToken: cancellationToken);

        return Success();
    }
}