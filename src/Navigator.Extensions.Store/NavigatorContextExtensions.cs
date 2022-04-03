using Microsoft.EntityFrameworkCore;
using Navigator.Context;
using Navigator.Extensions.Store.Bundled;
using Navigator.Extensions.Store.Entities;

namespace Navigator.Extensions.Store;

/// <summary>
/// Useful extensions for Navigator Context.
/// </summary>
public static class NavigatorContextExtensions
{
    #region Store

    public static IUniversalStore GetStore(this INavigatorContext context)
    {
        return GetStoreOrDefault(context) ?? throw new InvalidOperationException();
    }
    
    public static IUniversalStore? GetStoreOrDefault(this INavigatorContext context)
    {
        var value = context.Extensions.GetValueOrDefault(StoreContextExtension.StoreKey);
    
        if (value is IUniversalStore store)
        {
            return store;
        }

        return default;
    }

    #endregion

    #region Chat

    public static async Task<Chat> GetUniversalChat(this INavigatorContext context, CancellationToken cancellationToken = default)
    {
        return await GetUniversalChatOrDefault(context) ?? throw new InvalidOperationException();
    }
    
    public static async Task<Chat?> GetUniversalChatOrDefault(this INavigatorContext context, CancellationToken cancellationToken = default)
    {
        return await context.GetStore().FindChat(context.Conversation.Chat, context.Provider.Name, cancellationToken);
    }

    #endregion
    
    #region Conversation

    public static async Task<Conversation> GetUniversalConversation(this INavigatorContext context, CancellationToken cancellationToken = default)
    {
        return await GetUniversalConversationOrDefault(context, cancellationToken) ?? throw new InvalidOperationException();
    }
    
    public static async Task<Conversation?> GetUniversalConversationOrDefault(this INavigatorContext context, CancellationToken cancellationToken = default)
    {
        return await context.GetStore().FindConversation(context.Conversation, context.Provider.Name, cancellationToken);
    }

    #endregion
    
    #region User

    public static async Task<User> GetUniversalUser(this INavigatorContext context, CancellationToken cancellationToken = default)
    {
        return await GetUniversalUserOrDefault(context, cancellationToken) ?? throw new InvalidOperationException();
    }
    
    public static async Task<User?> GetUniversalUserOrDefault(this INavigatorContext context, CancellationToken cancellationToken = default)
    {
        return await context.GetStore().FindUser(context.Conversation.User, context.Provider.Name, cancellationToken);
    }

    #endregion
}