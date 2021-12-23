using Navigator.Entities;
using Navigator.Extensions.Store.Entities;

namespace Navigator.Extensions.Store;

public interface IUniversalStore
{
    #region Chat

    Task<UniversalChat?> FindChat(Chat chat, string provider, CancellationToken cancellationToken = default);

    #endregion
    
    #region Conversation

    Task<UniversalConversation> FindOrCreateConversation(Conversation conversation, string provider,
        CancellationToken cancellationToken = default);
    Task<UniversalConversation?> FindConversation(Conversation conversation, string provider,
        CancellationToken cancellationToken = default);
    
    #endregion

    #region User

    Task<UniversalUser?> FindUser(User user, string provider, CancellationToken cancellationToken = default);
    
    #endregion
    
    // #region Profile
    //
    // Task AddUserProfile(UniversalUser user, string provider, Guid identification, CancellationToken cancellationToken = default, bool? saveChanges = true);
    // Task TryAddUserProfile(UniversalUser user, string provider, Guid identification, CancellationToken cancellationToken = default, bool? saveChanges = true);
    //
    // Task AddChatProfile(UniversalChat chat, string provider, Guid identification, CancellationToken cancellationToken = default, bool? saveChanges = true);
    // Task TryAddChatProfile(UniversalChat chat, string provider, Guid identification, CancellationToken cancellationToken = default, bool? saveChanges = true);
    //
    // Task AddConversationProfile(UniversalConversation conversation, string provider, Guid identification, CancellationToken cancellationToken = default, bool? saveChanges = true);
    // Task TryAddConversationProfile(UniversalConversation conversation, string provider, Guid identification, CancellationToken cancellationToken = default, bool? saveChanges = true);
    //
    // #endregion
}