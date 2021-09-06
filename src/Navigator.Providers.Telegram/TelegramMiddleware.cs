using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Navigator.Actions;
using Navigator.Context;
using Navigator.Providers.Telegram.Actions;
using Navigator.Providers.Telegram.Actions.Message;
using Navigator.Providers.Telegram.Actions.Update;
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
                builder.SetOriginalEvent(update);
                builder.SetConversation(update.GetConversation());
            });

            await _actionLauncher.Launch();
        }

        private static string? DefineActionType(Update update)
        {
            return update.Type switch
            {
                UpdateType.Message when update.Message.Entities?.First()?.Type == MessageEntityType.BotCommand => typeof(CommandAction).FullName,
                UpdateType.Message => update.Message.Type switch
                {
                    // MessageType.Document => ActionType.ChatMembersAdded,
                    // MessageType.Location => ActionType.ChatMembersAdded,
                    // MessageType.Contact => ActionType.ChatMembersAdded,
                    // MessageType.Game => ActionType.ChatMembersAdded,
                    // MessageType.Invoice => ActionType.ChatMembersAdded,
                    // MessageType.SuccessfulPayment => ActionType.ChatMembersAdded,
                    // MessageType.WebsiteConnected => ActionType.ChatMembersAdded,
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
                    // MessageType.Dice => ActionType.MigratedFromGroup,
                    // MessageType.MessageAutoDeleteTimerChanged => ActionType.MigratedFromGroup,
                    // MessageType.ProximityAlertTriggered => ActionType.MigratedFromGroup,
                    // MessageType.VoiceChatScheduled => ActionType.MigratedFromGroup,
                    // MessageType.VoiceChatStarted => ActionType.MigratedFromGroup,
                    // MessageType.VoiceChatEnded => ActionType.MigratedFromGroup,
                    // MessageType.VoiceChatParticipantsInvited => ActionType.MigratedFromGroup,
                    _ => typeof(MessageAction).FullName,
                },
                UpdateType.InlineQuery => typeof(CommandAction).FullName,
                UpdateType.ChosenInlineResult => typeof(ChosenInlineResultAction).FullName,
                UpdateType.CallbackQuery => typeof(CallbackQueryAction).FullName,
                UpdateType.EditedMessage => typeof(EditedMessageAction).FullName,
                // UpdateType.ChannelPost => ActionType.ChannelPost,
                // UpdateType.EditedChannelPost => ActionType.EditedChannelPost,
                // UpdateType.ShippingQuery => ActionType.ShippingQuery,
                // UpdateType.PreCheckoutQuery => ActionType.PreCheckoutQuery,
                UpdateType.Poll => typeof(PollAction).FullName,
                // UpdateType.PollAnswer => typeof(PollAction).FullName,
                // UpdateType.MyChatMember => typeof(UnknownAction).FullName,
                // UpdateType.ChatMember => typeof(UnknownAction).FullName,
                UpdateType.Unknown => typeof(UnknownAction).FullName,
                _ => default
            };
        }
    }
}