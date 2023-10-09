using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Navigator.Actions;
using Navigator.Context.Accessor;
using Navigator.Extensions.Store;
using Telegram.Bot;

namespace Sample.Store.Actions;

public class EchoActionHandler : ActionHandler<EchoAction>
{
    public EchoActionHandler(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
    {
    }

    public override async Task<Status> Handle(EchoAction action, CancellationToken cancellationToken)
    {
        var firstInteraction = await Context.Store().Users
            .Where(e => e.Id == Context.Conversation.User.Id)
            .Select(user => user.FirstInteractionAt)
            .FirstAsync(cancellationToken);
        
        await Context.Client.SendTextMessageAsync(action.ChatId, $"{action.Text}\nFirst seen: {firstInteraction}", cancellationToken: cancellationToken);

        return Success();
    }
}