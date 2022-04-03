using Navigator.Entities;
using Navigator.Extensions.Store.Entities;
using Chat = Navigator.Extensions.Store.Entities.Chat;
using Conversation = Navigator.Extensions.Store.Entities.Conversation;
using User = Navigator.Extensions.Store.Entities.User;

namespace Navigator.Extensions.Store;

public interface IUniversalStore
{
    #region Chat

    Task<Chat?> FindChat(Navigator.Entities.Chat chat, string provider, CancellationToken cancellationToken = default);

    #endregion
    
    #region Conversation

    Task<Conversation> FindOrCreateConversation(Navigator.Entities.Conversation conversation, string provider,
        CancellationToken cancellationToken = default);
    Task<Conversation?> FindConversation(Navigator.Entities.Conversation conversation, string provider,
        CancellationToken cancellationToken = default);
    
    #endregion

    #region User

    Task<User?> FindUser(Navigator.Entities.User user, string provider, CancellationToken cancellationToken = default);
    
    #endregion
    
    // #region Profile
    //
    // Task AddUserProfile(User user, string provider, Guid identification, CancellationToken cancellationToken = default, bool? saveChanges = true);
    // Task TryAddUserProfile(User user, string provider, Guid identification, CancellationToken cancellationToken = default, bool? saveChanges = true);
    //
    // Task AddChatProfile(Chat chat, string provider, Guid identification, CancellationToken cancellationToken = default, bool? saveChanges = true);
    // Task TryAddChatProfile(Chat chat, string provider, Guid identification, CancellationToken cancellationToken = default, bool? saveChanges = true);
    //
    // Task AddConversationProfile(Conversation conversation, string provider, Guid identification, CancellationToken cancellationToken = default, bool? saveChanges = true);
    // Task TryAddConversationProfile(Conversation conversation, string provider, Guid identification, CancellationToken cancellationToken = default, bool? saveChanges = true);
    //
    // #endregion
}