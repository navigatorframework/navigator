namespace Navigator.Extensions.Store;

public interface IUniversalStore
{
    Task AddUserProfile(string provider, Guid identification, CancellationToken cancellationToken = default);
    Task TryAddUserProfile(string provider, Guid identification, CancellationToken cancellationToken = default);
    
    Task AddChatProfile(string provider, Guid identification, CancellationToken cancellationToken = default);
    Task TryAddChatProfile(string provider, Guid identification, CancellationToken cancellationToken = default);

    Task AddConversationProfile(string provider, Guid identification, CancellationToken cancellationToken = default);
    Task TryAddConversationProfile(string provider, Guid identification, CancellationToken cancellationToken = default);
}