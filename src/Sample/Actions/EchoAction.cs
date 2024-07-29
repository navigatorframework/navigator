using System.Threading;
using System.Threading.Tasks;
using Navigator.Actions;
using Navigator.Bundled.Extensions.Update;
using Navigator.Context.Accessor;
using Telegram.Bot;

namespace Sample.Actions;

public record EchoAction : Action
{
    public EchoAction(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
    {
    }
    
    public override bool CanHandleCurrentContext(CancellationToken cancellationToken = default)
    {
        return !string.IsNullOrWhiteSpace(Context.GetUpdate().Message!.Text);
    }

    public override async Task<Status> Handle(CancellationToken cancellationToken = default)
    {
        await Context.Client.SendTextMessageAsync(action.ChatId, action.Text, cancellationToken: cancellationToken);

        return Success();
    }
}