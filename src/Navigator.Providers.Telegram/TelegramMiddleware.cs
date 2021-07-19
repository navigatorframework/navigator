using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Navigator.Actions;
using Navigator.Context;
using Navigator.Providers.Telegram.Actions;
using Navigator.Providers.Telegram.Entities;
using Navigator.Providers.Telegram.Extensions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Navigator.Providers.Telegram
{
    internal class TelegramMiddleware
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
                builder.SetProvider<TelegramNavigatorProvider>();
                builder.SetActionType(actionType);
                builder.SetOriginalUpdate(update);
                builder.SetConversation(update.GetConversation());
            });

            await _actionLauncher.Launch();
        }

        private string? DefineActionType(Update update)
        {
            return update.Type switch
            {
                UpdateType.Message when update.Message.Entities?.First()?.Type == MessageEntityType.BotCommand => ActionsHelper.Type.For<TelegramNavigatorProvider>(nameof(CommandAction)),
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
                    _ => ActionsHelper.Type.For<TelegramNavigatorProvider>(nameof(MessageAction))
                },
                UpdateType.InlineQuery => ActionsHelper.Type.For<TelegramNavigatorProvider>(nameof(InlineQueryAction)),
                // UpdateType.ChosenInlineResult => ActionType.InlineResultChosen,
                UpdateType.CallbackQuery => ActionsHelper.Type.For<TelegramNavigatorProvider>(nameof(CallbackQueryAction)),
                UpdateType.EditedMessage => ActionsHelper.Type.For<TelegramNavigatorProvider>(nameof(EditedMessageAction)),
                // UpdateType.ChannelPost => ActionType.ChannelPost,
                // UpdateType.EditedChannelPost => ActionType.EditedChannelPost,
                // UpdateType.ShippingQuery => ActionType.ShippingQuery,
                // UpdateType.PreCheckoutQuery => ActionType.PreCheckoutQuery,
                UpdateType.Poll => ActionsHelper.Type.For<TelegramNavigatorProvider>(nameof(PollAction)),
                UpdateType.Unknown => ActionsHelper.Type.For<TelegramNavigatorProvider>(nameof(UnknownAction)),
                _ => default
            };
        }
    }
}