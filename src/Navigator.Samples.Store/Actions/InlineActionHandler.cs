using System;
using System.Threading;
using System.Threading.Tasks;
using Navigator.Actions;
using Navigator.Context;
using Navigator.Providers.Telegram;
using Telegram.Bot;
using Telegram.Bot.Types.InlineQueryResults;

namespace Navigator.Samples.Store.Actions;

public class InlineActionHandler : ActionHandler<InlineAction>
{
    public InlineActionHandler(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
    {
    }

    public override async Task<Status> Handle(InlineAction action, CancellationToken cancellationToken)
    {
        await NavigatorContext.GetTelegramClient().AnswerInlineQueryAsync(action.InlineQuery.Id, new InlineQueryResult[]
        {
            new InlineQueryResultArticle(Guid.NewGuid().ToString(), "Test", new InputTextMessageContent("Test Message"))
        }, cancellationToken: cancellationToken);

        return Success();
    }
}