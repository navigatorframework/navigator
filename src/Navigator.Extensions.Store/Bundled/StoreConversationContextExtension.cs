using Microsoft.EntityFrameworkCore;
using Navigator.Context;
using Navigator.Context.Builder.Options;
using Navigator.Extensions.Store.Context;
using Navigator.Extensions.Store.Entities;
using Navigator.Extensions.Store.Extractors;

namespace Navigator.Extensions.Store.Bundled;

internal class StoreConversationContextExtension : INavigatorContextExtension
{
    public const string UniversalConversation = "_navigator.extensions.store.source";

    private readonly NavigatorDbContext _dbContext;
    private readonly IEnumerable<IDataExtractor> _dataExtractors;

    public StoreConversationContextExtension(NavigatorDbContext dbContext, IEnumerable<IDataExtractor> dataExtractors)
    {
        _dbContext = dbContext;
        _dataExtractors = dataExtractors;
    }

    public async Task<INavigatorContext> Extend(INavigatorContext navigatorContext, INavigatorContextBuilderOptions builderOptions)
    {
        if (await _dbContext.Conversations.AnyAsync(e => e.Id == navigatorContext.Conversation.Id))
        {
            return navigatorContext;
        }

        var user = await TryStoreUserAsync(navigatorContext.Conversation);
        var chat = await TryStoreChatAsync(navigatorContext.Conversation);
        await TryStoreConversationAsync(navigatorContext.Conversation, user, chat);

        await _dbContext.SaveChangesAsync();
        
        return navigatorContext;
    }

    private async Task<User?> TryStoreUserAsync(Navigator.Entities.Conversation source)
    {
        if (await _dbContext.Users.AnyAsync(user => user.Id == source.User.Id)) return default;

        var user = new User
        {
            Id = source.User.Id
        };

        var data = _dataExtractors
            .FirstOrDefault(extractor => extractor.Maps(source.User.GetType()))?
            .From(source);

        if (data is not null)
        {
            foreach (var pair in data)
            {
                user.Data.Add(pair);
            }
        }

        await _dbContext.Users.AddAsync(user);

        return user;
    }
    
    private async Task<Chat?> TryStoreChatAsync(Navigator.Entities.Conversation source)
    {
        if (source.Chat is null) return default;

        if (await _dbContext.Chats.AnyAsync(chat => chat.Id == source.Chat.Id)) return default;
        
        var chat = new Chat
        {
            Id = source.Chat.Id
        };

        var data = _dataExtractors
            .FirstOrDefault(extractor => extractor.Maps(source.Chat.GetType()))?
            .From(source);

        if (data is not null)
        {
            foreach (var pair in data)
            {
                chat.Data.Add(pair);
            }
        }

        await _dbContext.Chats.AddAsync(chat);

        return chat;

    }

    private async Task TryStoreConversationAsync(Navigator.Entities.Conversation source, User? user, Chat? chat)
    {
        if (user is null || chat is null) return;

        var conversation = new Conversation(user, chat)
        {
            Id = source.Id,
            User = user,
            Chat = chat
        };

        var data = _dataExtractors
            .FirstOrDefault(extractor => extractor.Maps(source.GetType()))?
            .From(source);

        if (data is not null)
        {
            foreach (var pair in data)
            {
                chat.Data.Add(pair);
            }
        }

        await _dbContext.Conversations.AddAsync(conversation);
    }
}