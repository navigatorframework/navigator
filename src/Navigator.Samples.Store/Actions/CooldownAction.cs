using Navigator.Context;
using Navigator.Extensions.Cooldown;
using Navigator.Providers.Telegram.Actions.Messages;

namespace Navigator.Samples.Store.Actions;

[Cooldown(Seconds = 10)]
public class CooldownAction : CommandAction
{
    public CooldownAction(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
    {
    }

    public override bool CanHandleCurrentContext()
    {
        return Command.StartsWith("/cooldown", true, default);
    }
}