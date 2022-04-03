using System.Text;
using System.Text.Json;
using Navigator.Context;
using Navigator.Context.Extensions;
using Navigator.Extensions.Store.Context;

namespace Navigator.Extensions.Store.Bundled;

internal class UniversalConversationContextExtension : INavigatorContextExtension
{
    public const string UniversalConversation = "_navigator.extensions.store.universal_conversation";

    private readonly NavigatorDbContext _dbContext;

    public UniversalConversationContextExtension(NavigatorDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<INavigatorContext> Extend(INavigatorContext navigatorContext, INavigatorContextBuilderOptions builderOptions)
    {
        var conversation = navigatorContext.Conversation;

        var universalConversation = await _universalStore.FindOrCreateConversation(conversation, navigatorContext.Provider.Name);

        return await Task.FromResult(navigatorContext);
    }
    
    
}