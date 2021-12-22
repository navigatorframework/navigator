using Microsoft.EntityFrameworkCore;
using Navigator.Extensions.Store.Entities;

namespace Navigator.Extensions.Store;

public class UniversalStore : IUniversalStore
{
    private readonly NavigatorDbContext _dbContext;

    public UniversalStore(NavigatorDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddUserProfile(string provider, Guid identification, CancellationToken cancellationToken = default)
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
            Identification = identification,
        };

        await _dbContext.Profiles.AddAsync(profile, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task TryAddUserProfile(string provider, Guid identification, CancellationToken cancellationToken = default)
    {
        try
        {
            await AddUserProfile(provider, identification, cancellationToken);
        }
        catch (Exception e)
        {
            //TODO: log warning
        }
    }

    public async Task AddChatProfile(string provider, Guid identification, CancellationToken cancellationToken = default)
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

        await _dbContext.Profiles.AddAsync(profile, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task TryAddChatProfile(string provider, Guid identification, CancellationToken cancellationToken = default)
    {
        try
        {
            await AddChatProfile(provider, identification, cancellationToken);
        }
        catch (Exception e)
        {
            //TODO: log warning
        }
    }

    public async Task AddConversationProfile(string provider, Guid identification, CancellationToken cancellationToken = default)
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

        await _dbContext.Profiles.AddAsync(profile, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task TryAddConversationProfile(string provider, Guid identification, CancellationToken cancellationToken = default)
    {
        try
        {
            await AddConversationProfile(provider, identification, cancellationToken);
        }
        catch (Exception e)
        {
            //TODO: log warning
        }
    }
}