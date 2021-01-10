using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Navigator.Abstractions;
using Navigator.Extensions.Store.Abstractions;
using Navigator.Extensions.Store.Abstractions.Extensions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Chat = Navigator.Extensions.Store.Abstractions.Entity.Chat;
using User = Navigator.Extensions.Store.Abstractions.Entity.User;

namespace Navigator.Extensions.Store.Provider
{
    public class DefaultChatContextProvider<TUser, TChat> : INavigatorContextExtensionProvider
        where TUser : User
        where TChat : Chat
    {
        public int Order => 500;

        private readonly ILogger<DefaultChatContextProvider<TUser, TChat>> _logger;
        private readonly IEntityManager<TUser, TChat> _entityManager;

        public DefaultChatContextProvider(ILogger<DefaultChatContextProvider<TUser, TChat>> logger, IEntityManager<TUser, TChat> entityManager)
        {
            _logger = logger;
            _entityManager = entityManager;
        }

        public async Task<(string?, object?)> Process(Update update)
        {
            _logger.LogDebug("Processing update {UpdateId} of type {UpdateType} from {Provider}", update.Id, update.Type.ToString(), nameof(DefaultChatContextProvider<TUser, TChat>));

            TChat chat;

            switch (update.Type)
            {
                case UpdateType.Message:
                    await _entityManager.Handle(update.Message.From, update.Message.Chat);
                    chat = await _entityManager.FindChatAsync(update.Message.Chat.Id);
                    break;
                case UpdateType.EditedMessage:
                    await _entityManager.Handle(update.EditedMessage.From, update.EditedMessage.Chat);
                    chat = await _entityManager.FindChatAsync(update.EditedMessage.Chat.Id);
                    break;
                case UpdateType.ChannelPost:
                    await _entityManager.Handle(update.ChannelPost.From, update.ChannelPost.Chat);
                    chat = await _entityManager.FindChatAsync(update.ChannelPost.Chat.Id);
                    break;
                case UpdateType.EditedChannelPost:
                    await _entityManager.Handle(update.EditedChannelPost.From, update.EditedChannelPost.Chat);
                    chat = await _entityManager.FindChatAsync(update.EditedChannelPost.Chat.Id);
                    break;
                default:
                    return default;
            }

            return (INavigatorContextExtensions.DefaultChatKey, chat);
        }
    }
}