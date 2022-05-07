using System.Text.Json;
using Navigator.Entities;
using Navigator.Extensions.Store.Context;

namespace Navigator.Extensions.Store;

public class NavigatorStore : INavigatorStore
{
    private readonly NavigatorDbContext _dbContext;

    /// <summary>
    /// Default constructor.
    /// </summary>
    /// <param name="dbContext"></param>
    public NavigatorStore(NavigatorDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    /// <inheritdoc />
    public IDictionary<string, string>? GetAllData(User user)
    {
        return _dbContext.Users.Find(user.Id)?.Data;
    }

    /// <inheritdoc />
    public IDictionary<string, string>? GetAllData(Chat chat)
    {
        return _dbContext.Users.Find(chat.Id)?.Data;
    }

    /// <inheritdoc />
    public async Task<IDictionary<string, string>?> GetAllDataAsync(User user, CancellationToken cancellationToken = default)
    {
        return (await _dbContext.Users.FindAsync(new object?[] { user.Id }, cancellationToken))?.Data;
    }

    /// <inheritdoc />
    public async Task<IDictionary<string, string>?> GetAllDataAsync(Chat chat, CancellationToken cancellationToken = default)
    {
        return (await _dbContext.Chats.FindAsync(new object?[] { chat?.Id }, cancellationToken))?.Data;
    }

    /// <inheritdoc />
    public string? TryGetData(User user, string key)
    {
        var data = GetAllData(user);

        if (data?.TryGetValue(key, out var value) is not null)
        {
            return value;
        }

        return default;
    }

    public string? TryGetData(Chat chat, string key)
    {
        var data = GetAllData(chat);

        if (data?.TryGetValue(key, out var value) is not null)
        {
            return value;
        }

        return default;
    }

    /// <inheritdoc />
    public async Task<string?> TryGetDataAsync(User user, string key, CancellationToken cancellationToken = default)
    {
        var data = await GetAllDataAsync(user, cancellationToken);

        if (data?.TryGetValue(key, out var value) is not null)
        {
            return value;
        }

        return default;
    }

    /// <inheritdoc />
    public async Task<string?> TryGetDataAsync(Chat chat, string key, CancellationToken cancellationToken = default)
    {
        var data = await GetAllDataAsync(chat, cancellationToken);

        if (data?.TryGetValue(key, out var value) is not null)
        {
            return value;
        }

        return default;
    }

    /// <inheritdoc />
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

    /// <inheritdoc />
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

    /// <inheritdoc />
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

    /// <inheritdoc />
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