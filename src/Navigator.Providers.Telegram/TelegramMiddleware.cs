using Microsoft.Extensions.Logging;
using Navigator.Actions;
using Navigator.Context;
using Navigator.Providers.Telegram.Actions;
using Navigator.Providers.Telegram.Actions.Bundled;
using Navigator.Providers.Telegram.Actions.Messages;
using Navigator.Providers.Telegram.Actions.Updates;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.Payments;

namespace Navigator.Providers.Telegram;

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
            builder.SetProvider<TelegramNavigatorProvider>();
            builder.SetActionType(actionType);
            builder.SetOriginalEvent(update);
        });

        await _actionLauncher.Launch();
    }

    private static string? DefineActionType(Update update)
    {
        return update.Type switch
        {
            UpdateType.Message when update.Message?.Entities?.First().Type == MessageEntityType.BotCommand => nameof(CommandAction),
            UpdateType.Message => update.Message?.Type switch
            {
                MessageType.Document => nameof(DocumentAction),
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
                MessageType.SupergroupCreated => nameof(SupergroupCreatedAction),
                MessageType.ChannelCreated => nameof(ChannelCreatedAction),
                // MessageType.MigratedToSupergroup => ActionType.MigratedToSupergroup,
                // MessageType.MigratedFromGroup => ActionType.MigratedFromGroup,
                // MessageType.Dice => ActionType.MigratedFromGroup,
                // MessageType.MessageAutoDeleteTimerChanged => ActionType.MigratedFromGroup,
                // MessageType.ProximityAlertTriggered => ActionType.MigratedFromGroup,
                // MessageType.VoiceChatScheduled => ActionType.MigratedFromGroup,
                // MessageType.VoiceChatStarted => ActionType.MigratedFromGroup,
                // MessageType.VoiceChatEnded => ActionType.MigratedFromGroup,
                // MessageType.VoiceChatParticipantsInvited => ActionType.MigratedFromGroup,
                _ => nameof(MessageAction),
            },
            UpdateType.InlineQuery => nameof(InlineQueryAction),
            UpdateType.ChosenInlineResult => nameof(ChosenInlineResultAction),
            UpdateType.CallbackQuery => nameof(CallbackQueryAction),
            UpdateType.EditedMessage => nameof(EditedMessageAction),
            UpdateType.ChannelPost => nameof(ChannelPostAction),
            UpdateType.EditedChannelPost => nameof(EditedChannelPostAction),
            UpdateType.ShippingQuery => nameof(ShippingQueryAction),
            UpdateType.PreCheckoutQuery => nameof(PreCheckoutQuery),
            UpdateType.Poll => nameof(PollAction),
            UpdateType.PollAnswer => nameof(PollAnswerAction),
            UpdateType.MyChatMember => nameof(MyChatMemberAction),
            UpdateType.ChatMember => nameof(ChatMemberAction),
            UpdateType.ChatJoinRequest => nameof(ChatJoinRequestAction),
            UpdateType.Unknown => nameof(UnknownAction),
            _ => default
        };
    }
}