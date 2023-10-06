using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Navigator.Actions;
using Navigator.Context;
using Navigator.Extensions.Store.Bundled.Extensions;
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
        await this.GetTelegramClient().SendTextMessageAsync(this.GetTelegramChat().Id, 
            action.MessageToEcho, cancellationToken: cancellationToken);

        await this.GetTelegramClient().SendTextMessageAsync(this.GetTelegramChat().Id, 
            JsonSerializer.Serialize(await NavigatorContext.GetStore().GetDataAsync(NavigatorContext.Conversation.User, cancellationToken)), 
            cancellationToken: cancellationToken);

        return Success();
    }
}