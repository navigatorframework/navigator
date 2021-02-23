using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Navigator.Actions;
using Navigator.Context;
using Navigator.Providers.Telegram.Actions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Navigator.Providers.Telegram
{
    public class TelegramMiddleware
    {
        private readonly ILogger<TelegramMiddleware> _logger;
        private readonly INavigatorContextFactory _navigatorContextFactory;
        private readonly IActionLauncher _actionLauncher;

        public TelegramMiddleware(ILogger<TelegramMiddleware> logger, INavigatorContextFactory navigatorContextFactory, IActionLauncher actionLauncher)
        {
            _logger = logger;
            _navigatorContextFactory = navigatorContextFactory;
            _actionLauncher = actionLauncher;
        }

        public async Task Process(Update update)
        {
            var actionType = DefineActionType(update);

            if (actionType is null)
            {
                return;
            }
            
            await _navigatorContextFactory.Supply(builder =>
            {
                builder.SetProvider<TelegramProvider>();
                builder.SetActionType(actionType);
                builder.SetOriginalUpdate(update);
            });

            await _actionLauncher.Launch();
        }

        public string? DefineActionType(Update update)
        {
            return update.Type switch
            {
                UpdateType.Message when update.Message.Entities?.First()?.Type == MessageEntityType.BotCommand => ActionsHelper.Type.For<TelegramProvider>(nameof(CommandAction)),
                UpdateType.Message => update.Message.Type switch
                {
                    // MessageType.ChatMembersAdded => ActionType.ChatMembersAdded,
                    // MessageType.ChatMemberLeft => ActionType.ChatMemberLeft,
                    // MessageType.ChatTitleChanged => ActionType.ChatTitleChanged,
                    // MessageType.ChatPhotoChanged => ActionType.ChatPhotoChanged,
                    // MessageType.MessagePinned => ActionType.MessagePinned,
                    // MessageType.ChatPhotoDeleted => ActionType.ChatPhotoDeleted,
                    // MessageType.GroupCreated => ActionType.GroupCreated,
                    // MessageType.SupergroupCreated => ActionType.SupergroupCreated,
                    // MessageType.ChannelCreated => ActionType.ChannelCreated,
                    // MessageType.MigratedToSupergroup => ActionType.MigratedToSupergroup,
                    // MessageType.MigratedFromGroup => ActionType.MigratedFromGroup,
                    _ => ActionsHelper.Type.For<TelegramProvider>(nameof(MessageAction))
                },
                // UpdateType.InlineQuery => ActionType.InlineQuery,
                // UpdateType.ChosenInlineResult => ActionType.InlineResultChosen,
                // UpdateType.CallbackQuery => ActionType.CallbackQuery,
                // UpdateType.EditedMessage => ActionType.EditedMessage,
                // UpdateType.ChannelPost => ActionType.ChannelPost,
                // UpdateType.EditedChannelPost => ActionType.EditedChannelPost,
                // UpdateType.ShippingQuery => ActionType.ShippingQuery,
                // UpdateType.PreCheckoutQuery => ActionType.PreCheckoutQuery,
                // UpdateType.Poll => ActionType.Poll,
                // UpdateType.Unknown => ActionType.Unknown,
                _ => default
            };
        }
    }
}