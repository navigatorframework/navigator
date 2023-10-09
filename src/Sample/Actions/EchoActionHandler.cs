using System.Threading;
using System.Threading.Tasks;
using Navigator.Actions;
using Navigator.Context.Accessor;
using Telegram.Bot;

namespace Sample.Actions;

public class EchoActionHandler : ActionHandler<EchoAction>
{
    public EchoActionHandler(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
    {
    }

    public override async Task<Status> Handle(EchoAction action, CancellationToken cancellationToken)
    {
        await Context.Client.SendTextMessageAsync(action.ChatId, action.Text, cancellationToken: cancellationToken);

        return Success();
    }
}