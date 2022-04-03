using System.Text;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Navigator.Context;
using Navigator.Context.Extensions;
using Navigator.Extensions.Store.Context;

namespace Navigator.Extensions.Store.Bundled;

internal class StoreConversationContextExtension : INavigatorContextExtension
{
    public const string UniversalConversation = "_navigator.extensions.store.conversation";

    private readonly NavigatorDbContext _dbContext;

    public StoreConversationContextExtension(NavigatorDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<INavigatorContext> Extend(INavigatorContext navigatorContext, INavigatorContextBuilderOptions builderOptions)
    {
        var conversation = navigatorContext.Conversation;

        if (await _dbContext.Conversations.AnyAsync(e => e.Id == navigatorContext.Conversation.Id))
        {
            return navigatorContext;
        }
        else
        {
            
        }
        
        return await Task.FromResult(navigatorContext);
    }
    
    
}