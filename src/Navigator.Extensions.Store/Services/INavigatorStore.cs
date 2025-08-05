using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Hybrid;
using Microsoft.Extensions.Options;
using Navigator.Abstractions.Extensions;
using Navigator.Configuration.Options;
using Navigator.Extensions.Store.Entities;
using Navigator.Extensions.Store.Persistence.Context;

namespace Navigator.Extensions.Store.Services;

public interface INavigatorStore : INavigatorStore<NavigatorStoreDbContext> { }

public interface INavigatorStore<out TDbContext> where TDbContext : NavigatorStoreDbContext
{
    public TDbContext Context { get; }
    
    public Task<User?> GetUserAsync(long externalId, bool asTrackedEntity = false);
    public Task<Chat?> GetChatAsync(long externalId, bool asTrackedEntity = false);
    public Task<Conversation?> GetConversationAsync(long userExternalId, long? chatExternalId, bool asTrackedEntity = false);
}
public class NavigatorStore<TDbContext> : INavigatorStore<TDbContext> where TDbContext : NavigatorStoreDbContext
{
    private readonly HybridCache _cache;
    private readonly HybridCacheEntryOptions _cacheEntryOptions;

    public NavigatorStore(HybridCache cache, TDbContext context, IOptions<NavigatorOptions> navigatorOptions)
    {
        _cache = cache;
        Context = context;
        _cacheEntryOptions = navigatorOptions.Value
            .GetExtensionOptions<StoreExtension, StoreOptions>()?
            .RetrieveHybridCacheEntryOptions()
            ?? new HybridCacheEntryOptions();
    }

    public TDbContext Context { get; }
    
    public async Task<User?> GetUserAsync(long externalId, bool asTrackedEntity = false)
    {
        return await _cache.GetOrCreateAsync<User?>($"navigator.extensions.store.user:{externalId}", async _ =>
        {
            var user = await GetUserFromDatabase(externalId);
            return user;
        }, _cacheEntryOptions);
    }

    public async Task<Chat?> GetChatAsync(long externalId, bool asTrackedEntity = false)
    {
        return await _cache.GetOrCreateAsync<Chat?>($"navigator.extensions.store.chat:{externalId}", async _ =>
        {
            var chat = await GetChatFromDatabase(externalId);
            return chat;
        }, _cacheEntryOptions);
    }

    public async Task<Conversation?> GetConversationAsync(long userExternalId, long? chatExternalId, bool asTrackedEntity = false)
    {
        return await _cache.GetOrCreateAsync<Conversation?>($"navigator.extensions.store.conversation:{userExternalId}-{chatExternalId}", async _ =>
        {
            var conversation = await GetConversationFromDatabase(userExternalId, chatExternalId);
            return conversation;
        }, _cacheEntryOptions);
    }

    private async Task<User?> GetUserFromDatabase(long externalId, bool asTrackedEntity = false)
    {
        var query = asTrackedEntity
            ? Context.Users
            : Context.Users.AsNoTracking();
        
        return await query.FirstOrDefaultAsync(u => u.ExternalId == externalId);
    }
    
    private async Task<Chat?> GetChatFromDatabase(long externalId, bool asTrackedEntity = false)
    {
        var query = asTrackedEntity
            ? Context.Chats
            : Context.Chats.AsNoTracking();
        
        return await query.FirstOrDefaultAsync(c => c.ExternalId == externalId);
    }

    private async Task<Conversation?> GetConversationFromDatabase(long userExternalId, long? chatExternalId, bool asTrackedEntity = false)
    {
        var query = asTrackedEntity
            ? Context.Conversations
            : Context.Conversations.AsNoTracking();
        
        return chatExternalId is null
            ? await query
                .Where(c => c.User.ExternalId == userExternalId)
                .Where(c => c.Chat == null)
                .FirstOrDefaultAsync()
            : await query
                .Where(c => c.User.ExternalId == userExternalId)
                .Where(c => c.Chat!.ExternalId == chatExternalId)
                .FirstOrDefaultAsync();
    }
}

public class NavigatorStore : NavigatorStore<NavigatorStoreDbContext>, INavigatorStore
{
    public NavigatorStore(HybridCache cache, NavigatorStoreDbContext context, IOptions<NavigatorOptions> navigatorOptions)
        : base(cache, context, navigatorOptions)
    {
    }
}