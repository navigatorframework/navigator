using Navigator.Actions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.Passport;
using Telegram.Bot.Types.Payments;

namespace Navigator.Strategy.TypeProvider;

internal sealed record TelegramMessageTypeProvider : IArgumentTypeProvider
{
    /// <inheritdoc />
    public ushort Priority => 10000;

    /// <inheritdoc />
    public ValueTask<object?> GetArgument(Type type, Update update, BotAction action)
    {
        if (update.Type != UpdateType.Message) return ValueTask.FromResult<object?>(default);

        return ValueTask.FromResult<object?>(type switch
        {
            not null when type == typeof(string) && update.Message!.Type == MessageType.Text
                => update.Message.Text,
            not null when type == typeof(PhotoSize[]) && update.Message!.Type == MessageType.Photo
                => update.Message.Photo,
            not null when type == typeof(Audio) && update.Message!.Type == MessageType.Audio
                => update.Message.Audio,
            not null when type == typeof(Video) && update.Message!.Type == MessageType.Video
                => update.Message.Video,
            not null when type == typeof(Voice) && update.Message!.Type == MessageType.Voice
                => update.Message.Voice,
            not null when type == typeof(Document) && update.Message!.Type == MessageType.Document
                => update.Message.Document,
            not null when type == typeof(Sticker) && update.Message!.Type == MessageType.Sticker
                => update.Message.Sticker,
            not null when type == typeof(Location) && update.Message!.Type == MessageType.Location
                => update.Message.Location,
            not null when type == typeof(Contact) && update.Message!.Type == MessageType.Contact
                => update.Message.Contact,
            not null when type == typeof(Venue) && update.Message!.Type == MessageType.Venue
                => update.Message.Venue,
            not null when type == typeof(Game) && update.Message!.Type == MessageType.Game
                => update.Message.Game,
            not null when type == typeof(VideoNote) && update.Message!.Type == MessageType.VideoNote
                => update.Message.VideoNote,
            not null when type == typeof(Invoice) && update.Message!.Type == MessageType.Invoice
                => update.Message.Invoice,
            not null when type == typeof(SuccessfulPayment) && update.Message!.Type == MessageType.SuccessfulPayment
                => update.Message.SuccessfulPayment,
            not null when type == typeof(string) && update.Message!.Type == MessageType.ConnectedWebsite
                => update.Message.ConnectedWebsite,
            not null when type == typeof(User[]) && update.Message!.Type == MessageType.NewChatMembers
                => update.Message.NewChatMembers,
            not null when type == typeof(User) && update.Message!.Type == MessageType.LeftChatMember
                => update.Message.LeftChatMember,
            not null when type == typeof(string) && update.Message!.Type == MessageType.NewChatTitle
                => update.Message.NewChatTitle,
            not null when type == typeof(PhotoSize[]) && update.Message!.Type == MessageType.NewChatPhoto
                => update.Message.NewChatPhoto,
            not null when type == typeof(Message) && update.Message!.Type == MessageType.PinnedMessage
                => update.Message.PinnedMessage,
            not null when type == typeof(PhotoSize[]) && update.Message!.Type == MessageType.DeleteChatPhoto
                => update.Message.DeleteChatPhoto,
            not null when type == typeof(Chat) && update.Message!.Type == MessageType.GroupChatCreated
                => update.Message.Chat,
            not null when type == typeof(Chat) && update.Message!.Type == MessageType.SupergroupChatCreated
                => update.Message.Chat,
            not null when type == typeof(Chat) && update.Message!.Type == MessageType.ChannelChatCreated
                => update.Message.Chat,
            not null when type == typeof(long) && update.Message!.Type == MessageType.MigrateFromChatId
                => update.Message.MigrateFromChatId,
            not null when type == typeof(long) && update.Message!.Type == MessageType.MigrateToChatId
                => update.Message.MigrateToChatId,
            not null when type == typeof(Poll) && update.Message!.Type == MessageType.Poll
                => update.Message.Poll,
            not null when type == typeof(Dice) && update.Message!.Type == MessageType.Dice
                => update.Message.Dice,
            not null when type == typeof(MessageAutoDeleteTimerChanged) && update.Message!.Type == MessageType.MessageAutoDeleteTimerChanged
                => update.Message.MessageAutoDeleteTimerChanged,
            not null when type == typeof(ProximityAlertTriggered) && update.Message!.Type == MessageType.ProximityAlertTriggered
                => update.Message.ProximityAlertTriggered,
            not null when type == typeof(WebAppData) && update.Message!.Type == MessageType.WebAppData
                => update.Message.WebAppData,
            not null when type == typeof(VideoChatScheduled) && update.Message!.Type == MessageType.VideoChatScheduled
                => update.Message.VideoChatScheduled,
            not null when type == typeof(VideoChatStarted) && update.Message!.Type == MessageType.VideoChatStarted
                => update.Message.VideoChatStarted,
            not null when type == typeof(VideoChatEnded) && update.Message!.Type == MessageType.VideoChatEnded
                => update.Message.VideoChatEnded,
            not null when type == typeof(VideoChatParticipantsInvited) && update.Message!.Type == MessageType.VideoChatParticipantsInvited
                => update.Message.VideoChatParticipantsInvited,
            not null when type == typeof(Animation) && update.Message!.Type == MessageType.Animation
                => update.Message.Animation,
            not null when type == typeof(ForumTopicCreated) && update.Message!.Type == MessageType.ForumTopicCreated
                => update.Message.ForumTopicCreated,
            not null when type == typeof(ForumTopicClosed) && update.Message!.Type == MessageType.ForumTopicClosed
                => update.Message.ForumTopicClosed,
            not null when type == typeof(ForumTopicReopened) && update.Message!.Type == MessageType.ForumTopicReopened
                => update.Message.ForumTopicReopened,
            not null when type == typeof(ForumTopicEdited) && update.Message!.Type == MessageType.ForumTopicEdited
                => update.Message.ForumTopicEdited,
            not null when type == typeof(GeneralForumTopicHidden) && update.Message!.Type == MessageType.GeneralForumTopicHidden
                => update.Message.GeneralForumTopicHidden,
            not null when type == typeof(GeneralForumTopicUnhidden) && update.Message!.Type == MessageType.GeneralForumTopicUnhidden
                => update.Message.GeneralForumTopicUnhidden,
            not null when type == typeof(WriteAccessAllowed) && update.Message!.Type == MessageType.WriteAccessAllowed
                => update.Message.WriteAccessAllowed,
            not null when type == typeof(UsersShared) && update.Message!.Type == MessageType.UsersShared
                => update.Message.UsersShared,
            not null when type == typeof(ChatShared) && update.Message!.Type == MessageType.ChatShared
                => update.Message.ChatShared,
            not null when type == typeof(Story) && update.Message!.Type == MessageType.Story
                => update.Message.Story,
            not null when type == typeof(PassportData) && update.Message!.Type == MessageType.PassportData
                => update.Message.PassportData,
            not null when type == typeof(GiveawayCreated) && update.Message!.Type == MessageType.GiveawayCreated
                => update.Message.GiveawayCreated,
            not null when type == typeof(Giveaway) && update.Message!.Type == MessageType.Giveaway
                => update.Message.Giveaway,
            not null when type == typeof(GiveawayWinners) && update.Message!.Type == MessageType.GiveawayWinners
                => update.Message.GiveawayWinners,
            not null when type == typeof(GiveawayCompleted) && update.Message!.Type == MessageType.GiveawayCompleted
                => update.Message.GiveawayCompleted,
            not null when type == typeof(ChatBoostAdded) && update.Message!.Type == MessageType.BoostAdded
                => update.Message.BoostAdded,
            not null when type == typeof(ChatBackground) && update.Message!.Type == MessageType.ChatBackgroundSet
                => update.Message.ChatBackgroundSet,
            not null when type == typeof(PaidMedia) && update.Message!.Type == MessageType.PaidMedia
                => update.Message.PaidMedia,
            not null when type == typeof(RefundedPayment) && update.Message!.Type == MessageType.RefundedPayment
                => update.Message.RefundedPayment,
            _ => default
        });
    }
}