using System.Text;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Navigator.Context;
using Navigator.Context.Extensions;
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

        var user = await StoreUserAsync(navigatorContext.Conversation);
        var chat = await StoreChatAsync(navigatorContext.Conversation);
        await StoreConversationAsync(navigatorContext.Conversation, user, chat);

        await _dbContext.SaveChangesAsync();
        
        return navigatorContext;
    }

    private async Task<User> StoreUserAsync(Navigator.Entities.Conversation source)
    {
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
    
    private async Task<Chat> StoreChatAsync(Navigator.Entities.Conversation source)
    {
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

    private async Task StoreConversationAsync(Navigator.Entities.Conversation source, User user, Chat chat)
    {
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