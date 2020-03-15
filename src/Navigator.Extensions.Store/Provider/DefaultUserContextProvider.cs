using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Navigator.Abstraction;
using Navigator.Extensions.Store.Abstraction;
using Navigator.Extensions.Store.Context;
using Navigator.Extensions.Store.Entity;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Chat = Navigator.Extensions.Store.Entity.Chat;
using User = Navigator.Extensions.Store.Entity.User;

namespace Navigator.Extensions.Store.Provider
{
    public class DefaultUserContextProvider<TContext, TUser, TChat> : INavigatorContextExtensionProvider
        where TContext : NavigatorDbContext<TUser, TChat>
        where TUser : User
        where TChat : Chat
    {
        public int Order => 500;
        public static string DefaultUserKey => "navigator.extensions.store.user";

        private readonly ILogger<DefaultUserContextProvider<TContext, TUser, TChat>> _logger;
        private readonly TContext _navigatorDbContext;
        private readonly IUserMapper<TUser> _userMapper;
        private readonly IChatMapper<TChat> _chatMapper;

        public DefaultUserContextProvider(ILogger<DefaultUserContextProvider<TContext, TUser, TChat>> logger, TContext navigatorDbContext, IUserMapper<TUser> userMapper, IChatMapper<TChat> chatMapper)
        {
            _logger = logger;
            _navigatorDbContext = navigatorDbContext;
            _userMapper = userMapper;
            _chatMapper = chatMapper;
        }

        public async Task<(string?, object?)> Process(Update update)
        {
            TUser user;
            
            switch (update.Type)
            {
                case UpdateType.Message:
                    await Handle(update.Message.From, update.Message.Chat);
                    user = await _navigatorDbContext.Users.FindAsync(update.Message.From.Id);
                    break;
                case UpdateType.InlineQuery:
                    await Handle(update.InlineQuery.From);
                    user = await _navigatorDbContext.Users.FindAsync(update.InlineQuery.From.Id);
                    break;
                case UpdateType.ChosenInlineResult:
                    await Handle(update.ChosenInlineResult.From);
                    user = await _navigatorDbContext.Users.FindAsync(update.ChosenInlineResult.From.Id);
                    break;
                case UpdateType.CallbackQuery:
                    await Handle(update.CallbackQuery.From);
                    user = await _navigatorDbContext.Users.FindAsync(update.CallbackQuery.From.Id);
                    break;
                case UpdateType.EditedMessage:
                    await Handle(update.EditedMessage.From, update.EditedMessage.Chat);
                    user = await _navigatorDbContext.Users.FindAsync(update.EditedMessage.From.Id);
                    break;
                case UpdateType.ChannelPost:
                    await Handle(update.ChannelPost.From, update.ChannelPost.Chat);
                    user = await _navigatorDbContext.Users.FindAsync(update.ChannelPost.From.Id);
                    break;
                case UpdateType.EditedChannelPost:
                    await Handle(update.EditedChannelPost.From, update.EditedChannelPost.Chat);
                    user = await _navigatorDbContext.Users.FindAsync(update.EditedChannelPost.From.Id);
                    break;
                case UpdateType.ShippingQuery:
                    await Handle(update.ShippingQuery.From);
                    user = await _navigatorDbContext.Users.FindAsync(update.ShippingQuery.From.Id);
                    break;
                case UpdateType.PreCheckoutQuery:
                    await Handle(update.PreCheckoutQuery.From);
                    user = await _navigatorDbContext.Users.FindAsync(update.PreCheckoutQuery.From.Id);
                    break;
                default:
                    return default;
            }

            return (DefaultUserKey, user);
        }

        private async Task Handle(Telegram.Bot.Types.User telegramUser, CancellationToken cancellationToken = default)
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

        private async Task Handle(Telegram.Bot.Types.User telegramUser, Telegram.Bot.Types.Chat telegramChat,
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
    }
}