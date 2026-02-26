using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Hybrid;
using Microsoft.Extensions.Options;
using Navigator.Abstractions.Extensions;
using Navigator.Configuration.Options;
using Navigator.Extensions.Store.Entities;
using Navigator.Extensions.Store.Persistence.Context;

namespace Navigator.Extensions.Store.Services;

/// <summary>
///     Navigator store service using the default <see cref="NavigatorStoreDbContext"/>.
/// </summary>
public interface INavigatorStore : INavigatorStore<NavigatorStoreDbContext> { }

/// <summary>
///     Provides access to persisted Telegram entities (users, chats, conversations) with caching.
/// </summary>
/// <typeparam name="TDbContext">The database context type.</typeparam>
public interface INavigatorStore<out TDbContext> where TDbContext : NavigatorStoreDbContext
{
    /// <summary>
    ///     The underlying database context.
    /// </summary>
    public TDbContext Context { get; }
    
    /// <summary>
    ///     Retrieves a user by their Telegram identifier.
    /// </summary>
    /// <param name="externalId">The Telegram user identifier.</param>
    /// <param name="asTrackedEntity">Whether to return a change-tracked entity.</param>
    /// <returns>The user, or <c>null</c> if not found.</returns>
    public Task<User?> GetUserAsync(long externalId, bool asTrackedEntity = false);

    /// <summary>
    ///     Retrieves a chat by its Telegram identifier.
    /// </summary>
    /// <param name="externalId">The Telegram chat identifier.</param>
    /// <param name="asTrackedEntity">Whether to return a change-tracked entity.</param>
    /// <returns>The chat, or <c>null</c> if not found.</returns>
    public Task<Chat?> GetChatAsync(long externalId, bool asTrackedEntity = false);

    /// <summary>
    ///     Retrieves a conversation by user and optional chat Telegram identifiers.
    /// </summary>
    /// <param name="userExternalId">The Telegram user identifier.</param>
    /// <param name="chatExternalId">The Telegram chat identifier, or <c>null</c> for private conversations.</param>
    /// <param name="asTrackedEntity">Whether to return a change-tracked entity.</param>
    /// <returns>The conversation, or <c>null</c> if not found.</returns>
    public Task<Conversation?> GetConversationAsync(long userExternalId, long? chatExternalId, bool asTrackedEntity = false);
}

/// <summary>
///     Default implementation of <see cref="INavigatorStore{TDbContext}"/> with hybrid caching.
/// </summary>
/// <typeparam name="TDbContext">The database context type.</typeparam>
public class NavigatorStore<TDbContext> : INavigatorStore<TDbContext> where TDbContext : NavigatorStoreDbContext
{
    private readonly HybridCache _cache;
    private readonly HybridCacheEntryOptions _cacheEntryOptions;

    /// <summary>
    ///     Creates a new <see cref="NavigatorStore{TDbContext}"/>.
    /// </summary>
    /// <param name="cache">The hybrid cache instance.</param>
    /// <param name="context">The database context.</param>
    /// <param name="navigatorOptions">The Navigator configuration options.</param>
    public NavigatorStore(HybridCache cache, TDbContext context, IOptions<NavigatorOptions> navigatorOptions)
    {
        _cache = cache;
        Context = context;
        _cacheEntryOptions = navigatorOptions.Value
            .GetExtensionOptions<StoreExtension, StoreOptions>()?
            .RetrieveHybridCacheEntryOptions()
            ?? new HybridCacheEntryOptions();
    }

    /// <inheritdoc />
    public TDbContext Context { get; }
    
    /// <inheritdoc />
    public async Task<User?> GetUserAsync(long externalId, bool asTrackedEntity = false)
    {
        return await _cache.GetOrCreateAsync<User?>($"navigator.extensions.store.user:{externalId}", async _ =>
        {
            var user = await GetUserFromDatabase(externalId);
            return user;
        }, _cacheEntryOptions);
    }

    /// <inheritdoc />
    public async Task<Chat?> GetChatAsync(long externalId, bool asTrackedEntity = false)
    {
        return await _cache.GetOrCreateAsync<Chat?>($"navigator.extensions.store.chat:{externalId}", async _ =>
        {
            var chat = await GetChatFromDatabase(externalId);
            return chat;
        }, _cacheEntryOptions);
    }

    /// <inheritdoc />
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

/// <summary>
///     Convenience implementation of <see cref="INavigatorStore"/> using the default <see cref="NavigatorStoreDbContext"/>.
/// </summary>
public class NavigatorStore : NavigatorStore<NavigatorStoreDbContext>, INavigatorStore
{
    /// <summary>
    ///     Creates a new <see cref="NavigatorStore"/>.
    /// </summary>
    /// <param name="cache">The hybrid cache instance.</param>
    /// <param name="context">The database context.</param>
    /// <param name="navigatorOptions">The Navigator configuration options.</param>
    public NavigatorStore(HybridCache cache, NavigatorStoreDbContext context, IOptions<NavigatorOptions> navigatorOptions)
        : base(cache, context, navigatorOptions)
    {
    }
}
