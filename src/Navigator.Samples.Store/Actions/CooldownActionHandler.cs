using System.Threading;
using System.Threading.Tasks;
using Navigator.Actions;
using Navigator.Context;
using Navigator.Providers.Telegram;
using Telegram.Bot;

namespace Navigator.Samples.Store.Actions;

public class CooldownActionHandler : ActionHandler<CooldownAction>
{
    public CooldownActionHandler(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
    {
    }

    public override async Task<Status> Handle(CooldownAction action, CancellationToken cancellationToken)
    {
        await this.GetTelegramClient().SendTextMessageAsync(this.GetTelegramChat().Id, 
            "not in cooldown", cancellationToken: cancellationToken);

        return Success();
    }
}