using System.Text;
using System.Text.Json;
using Navigator.Context;
using Navigator.Context.Extensions;

namespace Navigator.Extensions.Store.Bundled;

internal class UniversalConversationContextExtension : INavigatorContextExtension
{
    public const string UniversalConversation = "_navigator.extensions.store.universal_conversation";

    private readonly IUniversalStore _universalStore;

    public UniversalConversationContextExtension(IUniversalStore universalStore)
    {
        _universalStore = universalStore;
    }

    public async Task<INavigatorContext> Extend(INavigatorContext navigatorContext, INavigatorContextBuilderOptions builderOptions)
    {
        var conversation = navigatorContext.Conversation;

        var universalConversation = await _universalStore.FindOrCreateConversation(conversation, navigatorContext.Provider.Name);

        
        

        return await Task.FromResult(navigatorContext);
    }
}