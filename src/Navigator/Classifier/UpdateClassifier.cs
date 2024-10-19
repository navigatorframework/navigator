using Navigator.Abstractions.Actions;
using Navigator.Abstractions.Classifier;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Navigator.Strategy.Classifier;

/// <inheritdoc />
public class UpdateClassifier : IUpdateClassifier
{
    /// <inheritdoc />
    public Task<UpdateCategory> Process(Update update)
    {
        return Task.FromResult(update.Type switch
        {
            UpdateType.Unknown => new UpdateCategory(nameof(UpdateType), nameof(UpdateType.Unknown)),
            UpdateType.Message when update.Message?.Entities?.First().Type == MessageEntityType.BotCommand
                => new UpdateCategory(nameof(MessageType), nameof(MessageEntityType.BotCommand)),
            UpdateType.Message => update.Message?.Type switch
            {
                MessageType.Unknown => new UpdateCategory(nameof(MessageType), nameof(MessageType.Unknown)),
                MessageType.Text => new UpdateCategory(nameof(MessageType), nameof(MessageType.Text)),
                MessageType.Photo => new UpdateCategory(nameof(MessageType), nameof(MessageType.Photo)),
                MessageType.Audio => new UpdateCategory(nameof(MessageType), nameof(MessageType.Audio)),
                MessageType.Video => new UpdateCategory(nameof(MessageType), nameof(MessageType.Video)),
                MessageType.Voice => new UpdateCategory(nameof(MessageType), nameof(MessageType.Voice)),
                MessageType.Document => new UpdateCategory(nameof(MessageType), nameof(MessageType.Document)),
                MessageType.Sticker => new UpdateCategory(nameof(MessageType), nameof(MessageType.Sticker)),
                MessageType.Location => new UpdateCategory(nameof(MessageType), nameof(MessageType.Location)),
                MessageType.Contact => new UpdateCategory(nameof(MessageType), nameof(MessageType.Contact)),
                MessageType.Venue => new UpdateCategory(nameof(MessageType), nameof(MessageType.Venue)),
                MessageType.Game => new UpdateCategory(nameof(MessageType), nameof(MessageType.Game)),
                MessageType.VideoNote => new UpdateCategory(nameof(MessageType), nameof(MessageType.VideoNote)),
                MessageType.Invoice => new UpdateCategory(nameof(MessageType), nameof(MessageType.Invoice)),
                MessageType.SuccessfulPayment => new UpdateCategory(nameof(MessageType), nameof(MessageType.SuccessfulPayment)),
                MessageType.ConnectedWebsite => new UpdateCategory(nameof(MessageType), nameof(MessageType.ConnectedWebsite)),
                MessageType.NewChatMembers => new UpdateCategory(nameof(MessageType), nameof(MessageType.NewChatMembers)),
                MessageType.LeftChatMember => new UpdateCategory(nameof(MessageType), nameof(MessageType.LeftChatMember)),
                MessageType.NewChatTitle => new UpdateCategory(nameof(MessageType), nameof(MessageType.NewChatTitle)),
                MessageType.NewChatPhoto => new UpdateCategory(nameof(MessageType), nameof(MessageType.NewChatPhoto)),
                MessageType.PinnedMessage => new UpdateCategory(nameof(MessageType), nameof(MessageType.PinnedMessage)),
                MessageType.DeleteChatPhoto => new UpdateCategory(nameof(MessageType), nameof(MessageType.DeleteChatPhoto)),
                MessageType.GroupChatCreated => new UpdateCategory(nameof(MessageType), nameof(MessageType.GroupChatCreated)),
                MessageType.SupergroupChatCreated => new UpdateCategory(nameof(MessageType), nameof(MessageType.SupergroupChatCreated)),
                MessageType.ChannelChatCreated => new UpdateCategory(nameof(MessageType), nameof(MessageType.ChannelChatCreated)),
                MessageType.MigrateFromChatId => new UpdateCategory(nameof(MessageType), nameof(MessageType.MigrateFromChatId)),
                MessageType.MigrateToChatId => new UpdateCategory(nameof(MessageType), nameof(MessageType.MigrateToChatId)),
                MessageType.Poll => new UpdateCategory(nameof(MessageType), nameof(MessageType.Poll)),
                MessageType.Dice => new UpdateCategory(nameof(MessageType), nameof(MessageType.Dice)),
                MessageType.MessageAutoDeleteTimerChanged => new UpdateCategory(nameof(MessageType),
                    nameof(MessageType.MessageAutoDeleteTimerChanged)),
                MessageType.ProximityAlertTriggered => new UpdateCategory(nameof(MessageType), nameof(MessageType.ProximityAlertTriggered)),
                MessageType.WebAppData => new UpdateCategory(nameof(MessageType), nameof(MessageType.WebAppData)),
                MessageType.VideoChatScheduled => new UpdateCategory(nameof(MessageType), nameof(MessageType.VideoChatScheduled)),
                MessageType.VideoChatStarted => new UpdateCategory(nameof(MessageType), nameof(MessageType.VideoChatStarted)),
                MessageType.VideoChatEnded => new UpdateCategory(nameof(MessageType), nameof(MessageType.VideoChatEnded)),
                MessageType.VideoChatParticipantsInvited => new UpdateCategory(nameof(MessageType),
                    nameof(MessageType.VideoChatParticipantsInvited)),
                MessageType.Animation => new UpdateCategory(nameof(MessageType), nameof(MessageType.Animation)),
                MessageType.ForumTopicCreated => new UpdateCategory(nameof(MessageType), nameof(MessageType.ForumTopicCreated)),
                MessageType.ForumTopicClosed => new UpdateCategory(nameof(MessageType), nameof(MessageType.ForumTopicClosed)),
                MessageType.ForumTopicReopened => new UpdateCategory(nameof(MessageType), nameof(MessageType.ForumTopicReopened)),
                MessageType.ForumTopicEdited => new UpdateCategory(nameof(MessageType), nameof(MessageType.ForumTopicEdited)),
                MessageType.GeneralForumTopicHidden => new UpdateCategory(nameof(MessageType), nameof(MessageType.GeneralForumTopicHidden)),
                MessageType.GeneralForumTopicUnhidden => new UpdateCategory(nameof(MessageType),
                    nameof(MessageType.GeneralForumTopicUnhidden)),
                MessageType.WriteAccessAllowed => new UpdateCategory(nameof(MessageType), nameof(MessageType.WriteAccessAllowed)),
                MessageType.UsersShared => new UpdateCategory(nameof(MessageType), nameof(MessageType.UsersShared)),
                MessageType.ChatShared => new UpdateCategory(nameof(MessageType), nameof(MessageType.ChatShared)),
                MessageType.Story => new UpdateCategory(nameof(MessageType), nameof(MessageType.Story)),
                MessageType.PassportData => new UpdateCategory(nameof(MessageType), nameof(MessageType.PassportData)),
                MessageType.GiveawayCreated => new UpdateCategory(nameof(MessageType), nameof(MessageType.GiveawayCreated)),
                MessageType.Giveaway => new UpdateCategory(nameof(MessageType), nameof(MessageType.Giveaway)),
                MessageType.GiveawayWinners => new UpdateCategory(nameof(MessageType), nameof(MessageType.GiveawayWinners)),
                MessageType.GiveawayCompleted => new UpdateCategory(nameof(MessageType), nameof(MessageType.GiveawayCompleted)),
                MessageType.BoostAdded => new UpdateCategory(nameof(MessageType), nameof(MessageType.BoostAdded)),
                MessageType.ChatBackgroundSet => new UpdateCategory(nameof(MessageType), nameof(MessageType.ChatBackgroundSet)),
                MessageType.PaidMedia => new UpdateCategory(nameof(MessageType), nameof(MessageType.PaidMedia)),
                MessageType.RefundedPayment => new UpdateCategory(nameof(MessageType), nameof(MessageType.RefundedPayment)),
                _ => new UpdateCategory(nameof(MessageType), nameof(MessageType.Unknown))
            },
            UpdateType.InlineQuery => new UpdateCategory(nameof(UpdateType), nameof(UpdateType.InlineQuery)),
            UpdateType.ChosenInlineResult => new UpdateCategory(nameof(UpdateType), nameof(UpdateType.ChosenInlineResult)),
            UpdateType.CallbackQuery => new UpdateCategory(nameof(UpdateType), nameof(UpdateType.CallbackQuery)),
            UpdateType.EditedMessage => new UpdateCategory(nameof(UpdateType), nameof(UpdateType.EditedMessage)),
            UpdateType.ChannelPost => new UpdateCategory(nameof(UpdateType), nameof(UpdateType.ChannelPost)),
            UpdateType.EditedChannelPost => new UpdateCategory(nameof(UpdateType), nameof(UpdateType.EditedChannelPost)),
            UpdateType.ShippingQuery => new UpdateCategory(nameof(UpdateType), nameof(UpdateType.ShippingQuery)),
            UpdateType.PreCheckoutQuery => new UpdateCategory(nameof(UpdateType), nameof(UpdateType.PreCheckoutQuery)),
            UpdateType.Poll => new UpdateCategory(nameof(UpdateType), nameof(UpdateType.Poll)),
            UpdateType.PollAnswer => new UpdateCategory(nameof(UpdateType), nameof(UpdateType.PollAnswer)),
            UpdateType.MyChatMember => new UpdateCategory(nameof(UpdateType), nameof(UpdateType.MyChatMember)),
            UpdateType.ChatMember => new UpdateCategory(nameof(UpdateType), nameof(UpdateType.ChatMember)),
            UpdateType.ChatJoinRequest => new UpdateCategory(nameof(UpdateType), nameof(UpdateType.ChatJoinRequest)),
            UpdateType.MessageReaction => new UpdateCategory(nameof(UpdateType), nameof(UpdateType.MessageReaction)),
            UpdateType.MessageReactionCount => new UpdateCategory(nameof(UpdateType), nameof(UpdateType.MessageReactionCount)),
            UpdateType.ChatBoost => new UpdateCategory(nameof(UpdateType), nameof(UpdateType.ChatBoost)),
            UpdateType.RemovedChatBoost => new UpdateCategory(nameof(UpdateType), nameof(UpdateType.RemovedChatBoost)),
            UpdateType.BusinessConnection => new UpdateCategory(nameof(UpdateType), nameof(UpdateType.BusinessConnection)),
            UpdateType.BusinessMessage => new UpdateCategory(nameof(UpdateType), nameof(UpdateType.BusinessMessage)),
            UpdateType.EditedBusinessMessage => new UpdateCategory(nameof(UpdateType), nameof(UpdateType.EditedBusinessMessage)),
            UpdateType.DeletedBusinessMessages => new UpdateCategory(nameof(UpdateType), nameof(UpdateType.DeletedBusinessMessages)),
            _ => new UpdateCategory(nameof(UpdateType), nameof(UpdateType.Unknown))
        });
    }
}