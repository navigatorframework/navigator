using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Navigator.Abstraction;
using Navigator.Extensions.Store.Abstraction;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Chat = Navigator.Extensions.Store.Entity.Chat;
using User = Navigator.Extensions.Store.Entity.User;

namespace Navigator.Extensions.Store.Provider
{
    public class DefaultUserContextProvider<TUser, TChat> : INavigatorContextExtensionProvider
        where TUser : User
        where TChat : Chat
    {
        public int Order => 500;

        private readonly ILogger<DefaultUserContextProvider<TUser, TChat>> _logger;
        private readonly IEntityManager<TUser, TChat> _entityManager;

        public DefaultUserContextProvider(ILogger<DefaultUserContextProvider<TUser, TChat>> logger, IEntityManager<TUser, TChat> entityManager)
        {
            _logger = logger;
            _entityManager = entityManager;
        }

        public async Task<(string?, object?)> Process(Update update)
        {
            _logger.LogDebug("Processing update {UpdateId} of type {UpdateType} from {Provider}", update.Id, update.Type.ToString(), nameof(DefaultChatContextProvider<TUser, TChat>));

            TUser user;
            
            switch (update.Type)
            {
                case UpdateType.Message:
                    await _entityManager.Handle(update.Message.From, update.Message.Chat);
                    user = await _entityManager.FindUserAsync(update.Message.From.Id);
                    break;
                case UpdateType.InlineQuery:
                    await _entityManager.Handle(update.InlineQuery.From);
                    user = await _entityManager.FindUserAsync(update.InlineQuery.From.Id);
                    break;
                case UpdateType.ChosenInlineResult:
                    await _entityManager.Handle(update.ChosenInlineResult.From);
                    user = await _entityManager.FindUserAsync(update.ChosenInlineResult.From.Id);
                    break;
                case UpdateType.CallbackQuery:
                    await _entityManager.Handle(update.CallbackQuery.From);
                    user = await _entityManager.FindUserAsync(update.CallbackQuery.From.Id);
                    break;
                case UpdateType.EditedMessage:
                    await _entityManager.Handle(update.EditedMessage.From, update.EditedMessage.Chat);
                    user = await _entityManager.FindUserAsync(update.EditedMessage.From.Id);
                    break;
                case UpdateType.ChannelPost:
                    await _entityManager.Handle(update.ChannelPost.From, update.ChannelPost.Chat);
                    user = await _entityManager.FindUserAsync(update.ChannelPost.From.Id);
                    break;
                case UpdateType.EditedChannelPost:
                    await _entityManager.Handle(update.EditedChannelPost.From, update.EditedChannelPost.Chat);
                    user = await _entityManager.FindUserAsync(update.EditedChannelPost.From.Id);
                    break;
                case UpdateType.ShippingQuery:
                    await _entityManager.Handle(update.ShippingQuery.From);
                    user = await _entityManager.FindUserAsync(update.ShippingQuery.From.Id);
                    break;
                case UpdateType.PreCheckoutQuery:
                    await _entityManager.Handle(update.PreCheckoutQuery.From);
                    user = await _entityManager.FindUserAsync(update.PreCheckoutQuery.From.Id);
                    break;
                default:
                    return default;
            }

            return (NavigatorContextExtensions.DefaultUserKey, user);
        }
    }
}