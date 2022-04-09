using System.Text.Json;
using System.Text.Json.Serialization;
using Navigator.Context;
using Navigator.Entities;
using Navigator.Extensions.Store.Context;

namespace Navigator.Extensions.Store;

public class NavigatorStore : INavigatorStore
{
    private readonly INavigatorContextAccessor _navigatorContextAccessor;
    private readonly NavigatorDbContext _dbContext;

    public NavigatorStore(INavigatorContextAccessor navigatorContextAccessor, NavigatorDbContext dbContext)
    {
        _navigatorContextAccessor = navigatorContextAccessor;
        _dbContext = dbContext;
    }

    public IDictionary<string, string>? GetData(User? user = default)
    {
        user ??= _navigatorContextAccessor.NavigatorContext.Conversation.User;

        return _dbContext.Users.Find(user.Id)?.Data;
    }

    public IDictionary<string, string>? GetData(Chat? chat = default)
    {
        chat ??= _navigatorContextAccessor.NavigatorContext.Conversation.Chat;

        return _dbContext.Users.Find(chat?.Id)?.Data;
    }

    public async Task<IDictionary<string, string>?> GetDataAsync(User? user = default, CancellationToken cancellationToken = default)
    {
        user ??= _navigatorContextAccessor.NavigatorContext.Conversation.User;

        return (await _dbContext.Users.FindAsync(new object?[] { user.Id }, cancellationToken))?.Data;
    }

    public async Task<IDictionary<string, string>?> GetDataAsync(Chat? chat = default, CancellationToken cancellationToken = default)
    {
        chat ??= _navigatorContextAccessor.NavigatorContext.Conversation.Chat;

        return (await _dbContext.Chats.FindAsync(new object?[] { chat?.Id }, cancellationToken))?.Data;
    }

    public bool TryAddData(User user, string key, object data, bool force = false)
    {
        var storedUser = _dbContext.Users.Find(user.Id);

        if (storedUser is null) return false;

        try
        {
            switch (force)
            {
                case true:
                    if (storedUser.Data.ContainsKey(key)) storedUser.Data.Remove(key);
                    return TryAddKeyValue();
                case false:
                    return TryAddKeyValue();
            }
        }
        catch (Exception)
        {
            return false;
        }

        bool TryAddKeyValue()
        {
            if (!storedUser.Data.TryAdd(key, JsonSerializer.Serialize(data)))
            {
                return false;
            }

            _dbContext.SaveChanges();

            return true;
        }
    }

    public bool TryAddData(Chat chat, string key, object data, bool force = false)
    {
        var storedChat = _dbContext.Chats.Find(chat.Id);

        if (storedChat is null) return false;

        try
        {
            switch (force)
            {
                case true:
                    if (storedChat.Data.ContainsKey(key)) storedChat.Data.Remove(key);
                    return TryAddKeyValue();
                case false:
                    return TryAddKeyValue();
            }
        }
        catch (Exception)
        {
            return false;
        }

        bool TryAddKeyValue()
        {
            if (!storedChat.Data.TryAdd(key, JsonSerializer.Serialize(data)))
            {
                return false;
            }

            _dbContext.SaveChanges();

            return true;
        }
    }

    public async Task<bool> TryAddDataAsync(User user, string key, object data, bool force = false, CancellationToken cancellationToken = default)
    {
        var storedUser = await _dbContext.Chats.FindAsync(new object?[] { user.Id }, cancellationToken);

        if (storedUser is null) return false;

        try
        {
            switch (force)
            {
                case true:
                    if (storedUser.Data.ContainsKey(key)) storedUser.Data.Remove(key);
                    return await TryAddKeyValueAsync();
                case false:
                    return await TryAddKeyValueAsync();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
        async Task<bool> TryAddKeyValueAsync()
        {
            if (!storedUser.Data.TryAdd(key, JsonSerializer.Serialize(data)))
            {
                return false;
            }

            await _dbContext.SaveChangesAsync(cancellationToken);

            return true;
        }
    }

    public async Task<bool> TryAddDataAsync(Chat chat, string key, object data, bool force = false, CancellationToken cancellationToken = default)
    {
        var storedChat = await _dbContext.Chats.FindAsync(new object?[] { chat.Id }, cancellationToken);

        if (storedChat is null) return false;

        try
        {
            switch (force)
            {
                case true:
                    if (storedChat.Data.ContainsKey(key)) storedChat.Data.Remove(key);
                    return await TryAddKeyValueAsync();
                case false:
                    return await TryAddKeyValueAsync();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
        async Task<bool> TryAddKeyValueAsync()
        {
            if (!storedChat.Data.TryAdd(key, JsonSerializer.Serialize(data)))
            {
                return false;
            }

            await _dbContext.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}