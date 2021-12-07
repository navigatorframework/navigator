using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Old.Navigator.Extensions.Store.Abstractions;
using Old.Navigator.Extensions.Store.Abstractions.Entity;
using Old.Navigator.Extensions.Store.Context;
using Telegram.Bot.Types;
using Chat = Old.Navigator.Extensions.Store.Abstractions.Entity.Chat;
using User = Old.Navigator.Extensions.Store.Abstractions.Entity.User;

namespace Old.Navigator.Extensions.Store
{
    public class DefaultEntityManager<TContext, TUser, TChat> : IEntityManager<TUser, TChat> where TContext : NavigatorDbContext<TUser, TChat>
        where TUser : User
        where TChat : Chat
    {
        private readonly ILogger<DefaultEntityManager<TContext, TUser, TChat>> _logger;
        private readonly TContext _navigatorDbContext;
        private readonly IUserMapper<TUser> _userMapper;
        private readonly IChatMapper<TChat> _chatMapper;

        public DefaultEntityManager(ILogger<DefaultEntityManager<TContext, TUser, TChat>> logger, TContext navigatorDbContext,
            IUserMapper<TUser> userMapper, IChatMapper<TChat> chatMapper)
        {
            _logger = logger;
            _navigatorDbContext = navigatorDbContext;
            _userMapper = userMapper;
            _chatMapper = chatMapper;
        }

        public async Task Handle(Telegram.Bot.Types.User telegramUser, CancellationToken cancellationToken = default)
        {
            _logger.LogDebug("Handling user {@UserId}", telegramUser.Id);

            var user = await _navigatorDbContext.Users.Include(u => u.Conversations)
                .FirstOrDefaultAsync(u => u.Id == telegramUser.Id, cancellationToken);

            if (user == null)
            {
                user = _userMapper.Parse(telegramUser);

                await _navigatorDbContext.AddAsync(user, cancellationToken);
                await _navigatorDbContext.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task Handle(Telegram.Bot.Types.User telegramUser, Telegram.Bot.Types.Chat telegramChat,
            CancellationToken cancellationToken = default)
        {
            _logger.LogDebug("Handling user {@UserId} and chat {@ChatId}", telegramUser.Id, telegramChat.Id);

            var userIsNew = false;
            var chatIsNew = false;
            var conversationIsNew = false;

            var user = await _navigatorDbContext.Users.Include(u => u.Conversations)
                .FirstOrDefaultAsync(u => u.Id == telegramUser.Id, cancellationToken);

            var chat = await _navigatorDbContext.Chats.Include(u => u.Conversations)
                .FirstOrDefaultAsync(u => u.Id == telegramChat.Id, cancellationToken);

            if (user == null)
            {
                userIsNew = true;
                user = _userMapper.Parse(telegramUser);
                await _navigatorDbContext.Users.AddAsync(user, cancellationToken);
            }

            if (chat == null)
            {
                chatIsNew = true;
                chat = _chatMapper.Parse(telegramChat);
                await _navigatorDbContext.Chats.AddAsync(chat, cancellationToken);
            }

            if (chat.Conversations.FirstOrDefault(cv => cv.UserId == user.Id) == null)
            {
                conversationIsNew = true;
                var conversation = new Conversation
                {
                    UserId = user.Id,
                    ChatId = chat.Id
                };
                await _navigatorDbContext.Conversations.AddAsync(conversation, cancellationToken);
            }

            if (userIsNew || chatIsNew || conversationIsNew)
            {
                await _navigatorDbContext.SaveChangesAsync(cancellationToken);
            }
        }

        public TUser FindUser(int id)
        {
            return _navigatorDbContext.Users.Find(id);
        }

        public async Task<TUser> FindUserAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _navigatorDbContext.Users.FindAsync(id);
        }

        public IEnumerable<TUser> FindAllUsersAsync(CancellationToken cancellationToken = default)
        {
            return _navigatorDbContext.Users.AsEnumerable();
        }

        public TChat FindChat(long id)
        {
            return _navigatorDbContext.Chats.Find(id);
        }

        public async Task<TChat> FindChatAsync(long id, CancellationToken cancellationToken = default)
        {
            return await _navigatorDbContext.Chats.FindAsync(id);
        }

        public IEnumerable<TChat> FindAllChatsAsync(CancellationToken cancellationToken = default)
        {
            return _navigatorDbContext.Chats.AsEnumerable();
        }

        public async Task MigrateFromGroup(Telegram.Bot.Types.Message telegramMessage, CancellationToken cancellationToken = default)
        {
            var chat = await FindChatAsync(telegramMessage.Chat.Id, cancellationToken);

            chat.Id = telegramMessage.MigrateToChatId;
            chat.Type = Chat.ChatType.Supergroup;

            await _navigatorDbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task MigrateToSupergroup(Telegram.Bot.Types.Message telegramMessage, CancellationToken cancellationToken = default)
        {
            var chat = await FindChatAsync(telegramMessage.MigrateFromChatId, cancellationToken);

            chat.Id = telegramMessage.Chat.Id;
            chat.Type = Chat.ChatType.Supergroup;

            await _navigatorDbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task ChatTitleChanged(Message telegramMessage, CancellationToken cancellationToken = default)
        {
            var chat = await FindChatAsync(telegramMessage.MigrateFromChatId, cancellationToken);

            chat.Title = telegramMessage.NewChatTitle;
            
            await _navigatorDbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task ChatMemberLeft(Message telegramMessage, CancellationToken cancellationToken = default)
        {
            var conversation = await _navigatorDbContext.Conversations
                .Where(c => c.ChatId == telegramMessage.Chat.Id && c.UserId == telegramMessage.From.Id)
                .SingleOrDefaultAsync(cancellationToken);

             _navigatorDbContext.Remove(conversation);
             
             await _navigatorDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}