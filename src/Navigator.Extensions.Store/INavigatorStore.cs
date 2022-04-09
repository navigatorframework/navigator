using Navigator.Entities;

namespace Navigator.Extensions.Store;

public interface INavigatorStore
{
    public IDictionary<string, string>? GetData(User user);
    public IDictionary<string, string>? GetData(Chat chat);
    public Task<IDictionary<string, string>?> GetDataAsync(User user, CancellationToken cancellationToken = default);
    public Task<IDictionary<string, string>?> GetDataAsync(Chat chat, CancellationToken cancellationToken = default);

    public bool TryAddData(User user, string key, object data, bool force = false);
    public bool TryAddData(Chat chat, string key, object data, bool force = false);
    public Task<bool> TryAddDataAsync(User user, string key, object data, bool force = false, CancellationToken cancellationToken = default);
    public Task<bool> TryAddDataAsync(Chat chat, string key, object data, bool force = false, CancellationToken cancellationToken = default);
}