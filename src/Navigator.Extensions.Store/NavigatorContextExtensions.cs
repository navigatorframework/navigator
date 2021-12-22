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

    public static NavigatorDbContext GetStore(this INavigatorContext context)
    {
        return GetStoreOrDefault(context) ?? throw new InvalidOperationException();
    }
    
    public static NavigatorDbContext? GetStoreOrDefault(this INavigatorContext context)
    {
        var value = context.Extensions.GetValueOrDefault(StoreContextExtension.StoreKey);
    
        if (value is NavigatorDbContext store)
        {
            return store;
        }

        return default;
    }

    #endregion

    #region User

    public static async Task<UniversalChat> GetUniversalChat(this INavigatorContext context)
    {
        return await GetUniversalChatOrDefault(context) ?? throw new InvalidOperationException();
    }
    
    public static async Task<UniversalChat?> GetUniversalChatOrDefault(this INavigatorContext context)
    {
        return await context.GetStore().Chats
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.Profiles
                .Any(p => p.Identification == context.Conversation.Chat.Id));
    }

    #endregion
    
    #region Conversation

    public static async Task<UniversalConversation> GetUniversalConversation(this INavigatorContext context)
    {
        return await GetUniversalConversationOrDefault(context) ?? throw new InvalidOperationException();
    }
    
    public static async Task<UniversalConversation?> GetUniversalConversationOrDefault(this INavigatorContext context)
    {
        return await context.GetStore().Users
            .AsNoTracking()
            .Where(e => e.Profiles
                .Any(p => p.Identification == context.Conversation.User.Id))
            .Select(e => e.Conversations.FirstOrDefault(c => c.Chat.Profiles
                .Any(p => p.Identification == context.Conversation.Chat.Id)))
            .FirstOrDefaultAsync();
    }

    #endregion
    
    #region User

    public static async Task<UniversalUser> GetUniversalUser(this INavigatorContext context)
    {
        return await GetUniversalUserOrDefault(context) ?? throw new InvalidOperationException();
    }
    
    public static async Task<UniversalUser?> GetUniversalUserOrDefault(this INavigatorContext context)
    {
        return await context.GetStore().Users
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.Profiles
                .Any(p => p.Identification == context.Conversation.User.Id));
    }

    #endregion
}