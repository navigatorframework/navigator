using Microsoft.EntityFrameworkCore;
using Navigator.Entities;
using Navigator.Extensions.Store.Context;
using Navigator.Extensions.Store.Entities;

namespace Navigator.Extensions.Store;

public class UniversalStore : IUniversalStore
{
    private readonly NavigatorDbContext _dbContext;

    public UniversalStore(NavigatorDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    #region Chat

    public async Task<UniversalChat?> FindChat(Chat chat, string provider, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Chats
            .Where(e => e.Profiles.Any(p => p.Provider == provider))
            .Where(e => e.Profiles.Any(p => p.Identification == chat.Id))
            .FirstOrDefaultAsync(cancellationToken);
    }
    
    #endregion
    
    #region Conversation

    public async Task<UniversalConversation> FindOrCreateConversation(Conversation conversation, string provider, CancellationToken cancellationToken = default)
    {
        var universalConversation = await FindConversation(conversation, provider, cancellationToken);

        if (universalConversation is null)
        {
            var user = new UniversalUser
            {
                Id = Guid.NewGuid()
            };
            
            user.Profiles.Add(new UserProfile
            {
                Id = Guid.NewGuid(),
                Provider = provider,
                Identification = conversation.User.Id
            });
            
            var chat = new UniversalChat
            {
                Id = Guid.NewGuid()
            };
            
            chat.Profiles.Add(new ChatProfile
            {
                Id = Guid.NewGuid(),
                Provider = provider,
                Identification = conversation.Chat.Id,
            });

            await _dbContext.Users.AddAsync(user, cancellationToken);
            await _dbContext.Chats.AddAsync(chat, cancellationToken);
            
            universalConversation = new UniversalConversation
            {
                User = user,
                Chat = chat,
            };
            
            universalConversation.Profiles.Add(new ConversationProfile
            {
                Id = Guid.NewGuid(),
                Provider = provider,
                Identification = Guid.NewGuid()
            });

            await _dbContext.Conversations.AddAsync(universalConversation, cancellationToken);
            
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        return universalConversation;
    }

    public async Task<UniversalConversation?> FindConversation(Conversation conversation, string provider, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Conversations
            .Include(e => e.User)
            .Include(e => e.Chat)
            .Where(e => e.Profiles.Any(p => p.Provider == provider))
            .Where(e => e.Chat.Profiles.Any(p => p.Provider == provider && p.Identification == conversation.Chat.Id))
            .Where(e => e.User.Profiles.Any(p => p.Provider == provider && p.Identification == conversation.User.Id))
            .FirstOrDefaultAsync(cancellationToken);
    }
    
    #endregion
    
    #region User

    public async Task<UniversalUser?> FindUser(User user, string provider, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Users
            .Where(e => e.Profiles.Any(p => p.Provider == provider))
            .Where(e => e.Profiles.Any(p => p.Identification == user.Id))
            .FirstOrDefaultAsync(cancellationToken);
    }
    
    #endregion
}