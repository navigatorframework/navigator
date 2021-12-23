using Microsoft.EntityFrameworkCore;
using Navigator.Entities;
using Navigator.Extensions.Store.Entities;

namespace Navigator.Extensions.Store;

public class UniversalStore : IUniversalStore
{
    private readonly NavigatorDbContext _dbContext;

    public UniversalStore(NavigatorDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
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

    #region Profile

    public async Task AddUserProfile(UniversalUser user, string provider, Guid identification, CancellationToken cancellationToken = default, bool? saveChanges = true)
    {
        var any = await _dbContext.Profiles.AnyAsync(e => e.Provider == provider && e.Identification == identification, cancellationToken);

        if (any)
        {
            throw new NavigatorStoreException("Profile already exists for this combination of provider and identification");
        }

        var profile = new UserProfile
        {
            Id = Guid.NewGuid(),
            Provider = provider,
            Identification = identification
        };
        
        user.Profiles.Add(profile);
        
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task TryAddUserProfile(UniversalUser user, string provider, Guid identification, CancellationToken cancellationToken = default, bool? saveChanges = true)
    {
        try
        {
            await AddUserProfile(user, provider, identification, cancellationToken, saveChanges);
        }
        catch (Exception e)
        {
            //TODO: log warning
        }
    }

    public async Task AddChatProfile(UniversalChat chat, string provider, Guid identification, CancellationToken cancellationToken = default, bool? saveChanges = true)
    {
        var any = await _dbContext.Profiles.AnyAsync(e => e.Provider == provider && e.Identification == identification, cancellationToken);

        if (any)
        {
            throw new NavigatorStoreException("Profile already exists for this combination of provider and identification");
        }

        var profile = new ChatProfile
        {
            Id = Guid.NewGuid(),
            Provider = provider,
            Identification = identification,
        };

        chat.Profiles.Add(profile);
        
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task TryAddChatProfile(UniversalChat chat, string provider, Guid identification, CancellationToken cancellationToken = default, bool? saveChanges = true)
    {
        try
        {
            await AddChatProfile(chat, provider, identification, cancellationToken, saveChanges);
        }
        catch (Exception e)
        {
            //TODO: log warning
        }
    }

    public async Task AddConversationProfile(UniversalConversation conversation, string provider, Guid identification, CancellationToken cancellationToken = default, bool? saveChanges = true)
    {
        var any = await _dbContext.Profiles.AnyAsync(e => e.Provider == provider && e.Identification == identification, cancellationToken);

        if (any)
        {
            throw new NavigatorStoreException("Profile already exists for this combination of provider and identification");
        }

        var profile = new ConversationProfile
        {
            Id = Guid.NewGuid(),
            Provider = provider,
            Identification = identification,
        };

        conversation.Profiles.Add(profile);

        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task TryAddConversationProfile(UniversalConversation conversation, string provider, Guid identification, CancellationToken cancellationToken = default, bool? saveChanges = true)
    {
        try
        {
            await AddConversationProfile(conversation, provider, identification, cancellationToken, saveChanges);
        }
        catch (Exception e)
        {
            //TODO: log warning
        }
    }

    #endregion
}