using Microsoft.Extensions.Logging;
using Navigator.Actions;
using Navigator.Bundled.Actions;
using Navigator.Bundled.Actions.Bundled;
using Navigator.Bundled.Actions.Messages;
using Navigator.Bundled.Actions.Updates;
using Navigator.Context;
using Navigator.Context.Builder.Options.Extensions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.Payments;

namespace Navigator;

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
            builder.SetActionType(actionType);
            builder.SetOriginalEvent(update);
        });

        await _actionLauncher.Launch();
    }

    private static string? DefineActionType(Update update)
    {
        return update.Type switch
        {
            UpdateType.Unknown => nameof(UnknownAction),
            UpdateType.Message when update.Message?.Entities?.First().Type == MessageEntityType.BotCommand => nameof(CommandAction),
            UpdateType.Message => update.Message?.Type switch
            {
                MessageType.Unknown => nameof(UnknownAction),
                MessageType.Text => nameof(TextAction),
                MessageType.Photo => nameof(PhotoAction),
                MessageType.Audio => nameof(AudioAction),
                MessageType.Video => nameof(VideoAction),
                MessageType.Voice => nameof(VoiceAction),
                MessageType.Document => nameof(DocumentAction),
                MessageType.Sticker => nameof(StickerAction),
                MessageType.Location => nameof(LocationAction),
                MessageType.Contact => nameof(ContactAction),
                MessageType.Venue => nameof(VenueAction),
                MessageType.Game => nameof(GameAction),
                MessageType.VideoNote => nameof(VideoNoteAction),
                MessageType.Invoice => nameof(InvoiceAction),
                MessageType.SuccessfulPayment => nameof(SuccessfulPaymentAction),
                MessageType.WebsiteConnected => nameof(WebsiteConnectedAction),
                MessageType.ChatMembersAdded => nameof(ChatMembersAddedAction),
                MessageType.ChatMemberLeft => nameof(ChatMemberLeftAction),
                MessageType.ChatTitleChanged => nameof(ChatTitleChangedAction),
                MessageType.ChatPhotoChanged => nameof(ChatPhotoChangedAction),
                MessageType.MessagePinned => nameof(MessagePinnedAction),
                MessageType.ChatPhotoDeleted => nameof(ChatPhotoDeletedAction),
                MessageType.GroupCreated => nameof(GroupCreatedAction),
                MessageType.SupergroupCreated => nameof(SupergroupCreatedAction),
                MessageType.ChannelCreated => nameof(ChannelCreatedAction),
                MessageType.MigratedToSupergroup => nameof(MigratedToSupergroupAction),
                MessageType.MigratedFromGroup => nameof(MigratedFromGroupAction),
                MessageType.Poll => nameof(PollMessageAction),
                MessageType.Dice => nameof(DiceAction),
                MessageType.MessageAutoDeleteTimerChanged => nameof(MessageAutoDeleteTimerChangedAction),
                MessageType.ProximityAlertTriggered => nameof(ProximityAlertTriggeredAction),
                MessageType.WebAppData => nameof(WebAppDataAction),
                MessageType.VideoChatScheduled => nameof(VideoChatScheduledAction),
                MessageType.VideoChatStarted => nameof(VideoChatStartedAction),
                MessageType.VideoChatEnded => nameof(VideoChatEndedAction),
                MessageType.VideoChatParticipantsInvited => nameof(VideoChatParticipantsInvitedAction),
                MessageType.Animation => nameof(AnimationAction),
                MessageType.ForumTopicCreated => nameof(ForumTopicCreatedAction),
                MessageType.ForumTopicClosed => nameof(ForumTopicClosedAction),
                MessageType.ForumTopicReopened => nameof(ForumTopicReopenedAction),
                MessageType.ForumTopicEdited => nameof(ForumTopicEditedAction),
                MessageType.GeneralForumTopicHidden => nameof(GeneralForumTopicHiddenAction),
                MessageType.GeneralForumTopicUnhidden => nameof(GeneralForumTopicUnhiddenAction),
                MessageType.WriteAccessAllowed => nameof(WriteAccessAllowedAction),
                // MessageType.UserShared => expr,
                // MessageType.ChatShared => expr,
                _ => nameof(MessageAction)
            },
            UpdateType.InlineQuery => nameof(InlineQueryAction),
            UpdateType.ChosenInlineResult => nameof(ChosenInlineResultAction),
            UpdateType.CallbackQuery => nameof(CallbackQueryAction),
            UpdateType.EditedMessage => nameof(EditedMessageAction),
            UpdateType.ChannelPost => nameof(ChannelPostAction),
            UpdateType.EditedChannelPost => nameof(EditedChannelPostAction),
            UpdateType.ShippingQuery => nameof(ShippingQueryAction),
            UpdateType.PreCheckoutQuery => nameof(PreCheckoutQuery),
            UpdateType.Poll => nameof(PollUpdateAction),
            UpdateType.PollAnswer => nameof(PollAnswerAction),
            UpdateType.MyChatMember => nameof(MyChatMemberAction),
            UpdateType.ChatMember => nameof(ChatMemberAction),
            UpdateType.ChatJoinRequest => nameof(ChatJoinRequestAction),
            _ => default
        };
    }
}