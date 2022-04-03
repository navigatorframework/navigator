// using Microsoft.AspNetCore.Components;
// using Microsoft.EntityFrameworkCore;
// using Navigator.Entities;
// using Navigator.Extensions.Store.Context;
// using Navigator.Extensions.Store.Entities;
// using Navigator.Extensions.Store.Mappers;
// using Chat = Navigator.Extensions.Store.Entities.Chat;
// using Conversation = Navigator.Extensions.Store.Entities.Conversation;
// using User = Navigator.Extensions.Store.Entities.User;
//
// namespace Navigator.Extensions.Store;
//
// public class UniversalStore : IUniversalStore
// {
//     private readonly NavigatorDbContext _dbContext;
//     private readonly IEnumerable<IProviderProfileMapper> _profileMappers;
//
//     public UniversalStore(NavigatorDbContext dbContext, IEnumerable<IProviderProfileMapper> profileMappers)
//     {
//         _dbContext = dbContext;
//         _profileMappers = profileMappers;
//     }
//     
//     #region Chat
//
//     public async Task<Chat?> FindChat(Navigator.Entities.Chat chat, string provider, CancellationToken cancellationToken = default)
//     {
//         return await _dbContext.Chats
//             .Where(e => e.Profiles.Any(p => p.Provider == provider))
//             .Where(e => e.Profiles.Any(p => p.Identification == chat.Id))
//             .FirstOrDefaultAsync(cancellationToken);
//     }
//     
//     #endregion
//     
//     #region Conversation
//
//     public async Task<Conversation> FindOrCreateConversation(Navigator.Entities.Conversation conversation, string provider, CancellationToken cancellationToken = default)
//     {
//         return await FindConversation(conversation, provider, cancellationToken) 
//                ?? await SaveConversation(conversation, provider, cancellationToken);
//     }
//
//     public async Task<Conversation?> FindConversation(Navigator.Entities.Conversation conversation, string provider, CancellationToken cancellationToken = default)
//     {
//         return await _dbContext.Conversations
//             .Include(e => e.User)
//             .Include(e => e.Chat)
//             .Where(e => e.Chat.Profiles.Any(p => p.Provider == provider && p.Identification == conversation.Chat.Id))
//             .Where(e => e.User.Profiles.Any(p => p.Provider == provider && p.Identification == conversation.User.Id))
//             .FirstOrDefaultAsync(cancellationToken);
//     }
//     
//     #endregion
//     
//     #region User
//
//     public async Task<User?> FindUser(Navigator.Entities.User user, string provider, CancellationToken cancellationToken = default)
//     {
//         return await _dbContext.Users
//             .Where(e => e.Profiles.Any(p => p.Provider == provider))
//             .Where(e => e.Profiles.Any(p => p.Identification == user.Id))
//             .FirstOrDefaultAsync(cancellationToken);
//     }
//
//     #endregion
//
//     protected async Task<Conversation> SaveConversation(Navigator.Entities.Conversation conversation, string provider, CancellationToken cancellationToken = default)
//     {
//         var userProfileMapper = _profileMappers
//             .OfType<IProviderUserProfileMapper>()
//             .FirstOrDefault(mapper => mapper.Maps(conversation.User.GetType()));
//         
//         var user = new User
//         {
//             Id = Guid.NewGuid()
//         };
//
//         user.Profiles.Add(userProfileMapper?.From(conversation) ?? throw new NavigationException("TODO"));
//         
//         
//         var chatProfileMapper = _profileMappers
//             .OfType<IProviderChatProfileMapper>()
//             .FirstOrDefault(mapper => mapper.Maps(conversation.Chat.GetType()));
//
//         var chat = new Chat
//         {
//             Id = Guid.NewGuid()
//         };
//             
//         chat.Profiles.Add(chatProfileMapper?.From(conversation) ?? throw new NavigationException("TODO"));
//
//         await _dbContext.Users.AddAsync(user, cancellationToken);
//         await _dbContext.Chats.AddAsync(chat, cancellationToken);
//             
//         var universalConversation = new Conversation
//         {
//             Id = Guid.NewGuid(),
//             User = user,
//             Chat = chat,
//         };
//
//         var conversationProfileMapper = _profileMappers
//             .OfType<IProviderConversationProfileMapper>()
//             .FirstOrDefault(mapper => mapper.Maps(conversation.Chat.GetType()));
//         
//         universalConversation.Profiles.Add(conversationProfileMapper?.From(conversation) ?? throw new NavigationException("TODO"));
//
//         await _dbContext.Conversations.AddAsync(universalConversation, cancellationToken);
//             
//         await _dbContext.SaveChangesAsync(cancellationToken);
//
//         return universalConversation;
//     }
// }