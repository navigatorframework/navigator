using Navigator.Entities;

namespace Navigator.Extensions.Store;

public interface INavigatorStore
{
    public IDictionary<string, string>? GetAllData(User user);
    public IDictionary<string, string>? GetAllData(Chat chat);
    public Task<IDictionary<string, string>?> GetAllDataAsync(User user, CancellationToken cancellationToken = default);
    public Task<IDictionary<string, string>?> GetAllDataAsync(Chat chat, CancellationToken cancellationToken = default);
    
    public string? TryGetData(User user, string key);
    public string? TryGetData(Chat chat, string key);
    public Task<string?> TryGetDataAsync(User user, string key, CancellationToken cancellationToken = default);
    public Task<string?> TryGetDataAsync(Chat chat, string key, CancellationToken cancellationToken = default);
    
    public bool TryAddData(User user, string key, object data, bool force = false);
    public bool TryAddData(Chat chat, string key, object data, bool force = false);
    public Task<bool> TryAddDataAsync(User user, string key, object data, bool force = false, CancellationToken cancellationToken = default);
    public Task<bool> TryAddDataAsync(Chat chat, string key, object data, bool force = false, CancellationToken cancellationToken = default);
}