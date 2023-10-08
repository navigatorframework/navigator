using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Navigator.Context;
using Navigator.Context.Builder.Options;
using Navigator.Extensions.Store.Context;
using Navigator.Extensions.Store.Entities;

namespace Navigator.Extensions.Store;

internal class StoreConversationNavigatorContextExtension : INavigatorContextExtension
{
    public const string UniversalConversation = "_navigator.extensions.store.source";

    private readonly NavigatorDbContext _dbContext;
    private readonly IMemoryCache _memoryCache;

    public StoreConversationNavigatorContextExtension(NavigatorDbContext dbContext, IMemoryCache memoryCache)
    {
        _dbContext = dbContext;
        _memoryCache = memoryCache;
    }

    public async Task<INavigatorContext> Extend(INavigatorContext navigatorContext, INavigatorContextBuilderOptions builderOptions)
    {
        if (_memoryCache.Get(navigatorContext.Conversation.GetHashCode()) is not null)
        {
            return navigatorContext;
        }

        if (await _dbContext.Conversations
                .Where(e => e.User.Id == navigatorContext.Conversation.User.Id)
                .Where(e => navigatorContext.Conversation.Chat != null && e.Chat != null &&
                            e.Chat.Id == navigatorContext.Conversation.Chat.Id)
                .AnyAsync())
        {
            _memoryCache.Set(navigatorContext.Conversation.GetHashCode(), true);

            return navigatorContext;
        }

        var user = await TryStoreUserAsync(navigatorContext.Conversation);
        var chat = await TryStoreChatAsync(navigatorContext.Conversation);
        await TryStoreConversationAsync(user, chat);

        await _dbContext.SaveChangesAsync();

        _memoryCache.Set(navigatorContext.Conversation.GetHashCode(), true);

        return navigatorContext;
    }

    private async Task<User?> TryStoreUserAsync(Navigator.Entities.Conversation source)
    {
        if (await _dbContext.Users.AnyAsync(user => user.Id == source.User.Id)) return default;

        var user = source.User is Navigator.Entities.Bot bot
            ? new Bot(bot)
            : new User(source.User);

        await _dbContext.Users.AddAsync(user);

        return user;
    }

    private async Task<Chat?> TryStoreChatAsync(Navigator.Entities.Conversation source)
    {
        if (source.Chat is null) return default;

        if (await _dbContext.Chats.AnyAsync(chat => chat.Id == source.Chat.Id)) return default;

        var chat = new Chat(source.Chat);

        await _dbContext.Chats.AddAsync(chat);

        return chat;
    }

    private async Task TryStoreConversationAsync(User? user, Chat? chat)
    {
        if (user is null || chat is null) return;

        var conversation = new Conversation(user, chat);

        await _dbContext.Conversations.AddAsync(conversation);
    }
}