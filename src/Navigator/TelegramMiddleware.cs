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
                // MessageType.Venue => expr,
                // MessageType.Game => expr,
                // MessageType.VideoNote => expr,
                // MessageType.Invoice => expr,
                // MessageType.SuccessfulPayment => expr,
                // MessageType.WebsiteConnected => expr,
                // MessageType.ChatMembersAdded => expr,
                // MessageType.ChatMemberLeft => expr,
                // MessageType.ChatTitleChanged => expr,
                // MessageType.ChatPhotoChanged => expr,
                // MessageType.MessagePinned => expr,
                // MessageType.ChatPhotoDeleted => expr,
                // MessageType.GroupCreated => expr,
                MessageType.SupergroupCreated => nameof(SupergroupCreatedAction),
                MessageType.ChannelCreated => nameof(ChannelCreatedAction),
                // MessageType.MigratedToSupergroup => expr,
                // MessageType.MigratedFromGroup => expr,
                // MessageType.Poll => expr,
                // MessageType.Dice => expr,
                // MessageType.MessageAutoDeleteTimerChanged => expr,
                // MessageType.ProximityAlertTriggered => expr,
                // MessageType.WebAppData => expr,
                // MessageType.VideoChatScheduled => expr,
                // MessageType.VideoChatStarted => expr,
                // MessageType.VideoChatEnded => expr,
                // MessageType.VideoChatParticipantsInvited => expr,
                // MessageType.Animation => expr,
                // MessageType.ForumTopicCreated => expr,
                // MessageType.ForumTopicClosed => expr,
                // MessageType.ForumTopicReopened => expr,
                // MessageType.ForumTopicEdited => expr,
                // MessageType.GeneralForumTopicHidden => expr,
                // MessageType.GeneralForumTopicUnhidden => expr,
                // MessageType.WriteAccessAllowed => expr,
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
            UpdateType.Poll => nameof(PollAction),
            UpdateType.PollAnswer => nameof(PollAnswerAction),
            UpdateType.MyChatMember => nameof(MyChatMemberAction),
            UpdateType.ChatMember => nameof(ChatMemberAction),
            UpdateType.ChatJoinRequest => nameof(ChatJoinRequestAction),
            _ => default
        };
    }
}