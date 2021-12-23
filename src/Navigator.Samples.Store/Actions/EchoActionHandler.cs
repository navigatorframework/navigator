﻿using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Navigator.Actions;
using Navigator.Context;
using Navigator.Extensions.Store;
using Navigator.Providers.Telegram;
using Telegram.Bot;

namespace Navigator.Samples.Store.Actions;

public class EchoActionHandler : ActionHandler<EchoAction>
{
    private readonly NavigatorDbContext _dbContext;
    public EchoActionHandler(INavigatorContextAccessor navigatorContextAccessor, NavigatorDbContext dbContext) : base(navigatorContextAccessor)
    {
        _dbContext = dbContext;
    }

    public override async Task<Status> Handle(EchoAction action, CancellationToken cancellationToken)
    {
        var uc = await NavigatorContext.GetStoreOrDefault()?.FindConversation(NavigatorContext.Conversation, NavigatorContext.Provider.Name, cancellationToken);
        
        await this.GetTelegramClient().SendTextMessageAsync(this.GetTelegramChat().Id, 
             uc.FirstInteractionAt.ToLongDateString(), cancellationToken: cancellationToken);
        
        await this.GetTelegramClient().SendTextMessageAsync(this.GetTelegramChat().Id, 
            action.MessageToEcho, cancellationToken: cancellationToken);

        return Success();
    }
}