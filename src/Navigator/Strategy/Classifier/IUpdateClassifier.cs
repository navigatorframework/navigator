using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Navigator.Strategy.Classifier;

public interface IUpdateClassifier
{
    Task<string> Process(Update update);
}

public class UpdateClassifier : IUpdateClassifier
{
    public Task<string> Process(Update update)
    {
        return Task.FromResult(update.Type switch
        {
            UpdateType.Unknown => $"{typeof(UpdateType)}.{nameof(UpdateType.Unknown)}",
            UpdateType.Message when update.Message?.Entities?.First().Type == MessageEntityType.BotCommand 
                => $"{typeof(MessageType)}.{nameof(MessageEntityType.BotCommand)}",
            UpdateType.Message => update.Message?.Type switch
            {
                MessageType.Unknown => $"{typeof(MessageType)}.{nameof(MessageType.Unknown)}",
                MessageType.Text => $"{typeof(MessageType)}.{nameof(MessageType.Text)}",
                MessageType.Photo => $"{typeof(MessageType)}.{nameof(MessageType.Photo)}",
                MessageType.Audio => $"{typeof(MessageType)}.{nameof(MessageType.Audio)}",
                MessageType.Video => $"{typeof(MessageType)}.{nameof(MessageType.Video)}",
                MessageType.Voice => $"{typeof(MessageType)}.{nameof(MessageType.Voice)}",
                MessageType.Document => $"{typeof(MessageType)}.{nameof(MessageType.Document)}",
                MessageType.Sticker => $"{typeof(MessageType)}.{nameof(MessageType.Sticker)}",
                MessageType.Location => $"{typeof(MessageType)}.{nameof(MessageType.Location)}",
                MessageType.Contact => $"{typeof(MessageType)}.{nameof(MessageType.Contact)}",
                MessageType.Venue => $"{typeof(MessageType)}.{nameof(MessageType.Venue)}",
                MessageType.Game => $"{typeof(MessageType)}.{nameof(MessageType.Game)}",
                MessageType.VideoNote => $"{typeof(MessageType)}.{nameof(MessageType.VideoNote)}",
                MessageType.Invoice => $"{typeof(MessageType)}.{nameof(MessageType.Invoice)}",
                MessageType.SuccessfulPayment => $"{typeof(MessageType)}.{nameof(MessageType.SuccessfulPayment)}",
                MessageType.ConnectedWebsite => $"{typeof(MessageType)}.{nameof(MessageType.ConnectedWebsite)}",
                MessageType.NewChatMembers => $"{typeof(MessageType)}.{nameof(MessageType.NewChatMembers)}",
                MessageType.LeftChatMember => $"{typeof(MessageType)}.{nameof(MessageType.LeftChatMember)}",
                MessageType.NewChatTitle => $"{typeof(MessageType)}.{nameof(MessageType.NewChatTitle)}",
                MessageType.NewChatPhoto => $"{typeof(MessageType)}.{nameof(MessageType.NewChatPhoto)}",
                MessageType.PinnedMessage => $"{typeof(MessageType)}.{nameof(MessageType.PinnedMessage)}",
                MessageType.DeleteChatPhoto => $"{typeof(MessageType)}.{nameof(MessageType.DeleteChatPhoto)}",
                MessageType.GroupChatCreated => $"{typeof(MessageType)}.{nameof(MessageType.GroupChatCreated)}",
                MessageType.SupergroupChatCreated => $"{typeof(MessageType)}.{nameof(MessageType.SupergroupChatCreated)}",
                MessageType.ChannelChatCreated => $"{typeof(MessageType)}.{nameof(MessageType.ChannelChatCreated)}",
                MessageType.MigrateFromChatId => $"{typeof(MessageType)}.{nameof(MessageType.MigrateFromChatId)}",
                MessageType.MigrateToChatId => $"{typeof(MessageType)}.{nameof(MessageType.MigrateToChatId)}",
                MessageType.Poll => $"{typeof(MessageType)}.{nameof(MessageType.Poll)}",
                MessageType.Dice => $"{typeof(MessageType)}.{nameof(MessageType.Dice)}",
                MessageType.MessageAutoDeleteTimerChanged => $"{typeof(MessageType)}.{nameof(MessageType.MessageAutoDeleteTimerChanged)}",
                MessageType.ProximityAlertTriggered => $"{typeof(MessageType)}.{nameof(MessageType.ProximityAlertTriggered)}",
                MessageType.WebAppData => $"{typeof(MessageType)}.{nameof(MessageType.WebAppData)}",
                MessageType.VideoChatScheduled => $"{typeof(MessageType)}.{nameof(MessageType.VideoChatScheduled)}",
                MessageType.VideoChatStarted => $"{typeof(MessageType)}.{nameof(MessageType.VideoChatStarted)}",
                MessageType.VideoChatEnded => $"{typeof(MessageType)}.{nameof(MessageType.VideoChatEnded)}",
                MessageType.VideoChatParticipantsInvited => $"{typeof(MessageType)}.{nameof(MessageType.VideoChatParticipantsInvited)}",
                MessageType.Animation => $"{typeof(MessageType)}.{nameof(MessageType.Animation)}",
                MessageType.ForumTopicCreated => $"{typeof(MessageType)}.{nameof(MessageType.ForumTopicCreated)}",
                MessageType.ForumTopicClosed => $"{typeof(MessageType)}.{nameof(MessageType.ForumTopicClosed)}",
                MessageType.ForumTopicReopened => $"{typeof(MessageType)}.{nameof(MessageType.ForumTopicReopened)}",
                MessageType.ForumTopicEdited => $"{typeof(MessageType)}.{nameof(MessageType.ForumTopicEdited)}",
                MessageType.GeneralForumTopicHidden => $"{typeof(MessageType)}.{nameof(MessageType.GeneralForumTopicHidden)}",
                MessageType.GeneralForumTopicUnhidden => $"{typeof(MessageType)}.{nameof(MessageType.GeneralForumTopicUnhidden)}",
                MessageType.WriteAccessAllowed => $"{typeof(MessageType)}.{nameof(MessageType.WriteAccessAllowed)}",
                MessageType.UsersShared => $"{typeof(MessageType)}.{nameof(MessageType.UsersShared)}",
                MessageType.ChatShared => $"{typeof(MessageType)}.{nameof(MessageType.ChatShared)}",
                MessageType.Story => $"{typeof(MessageType)}.{nameof(MessageType.Story)}",
                MessageType.PassportData => $"{typeof(MessageType)}.{nameof(MessageType.PassportData)}",
                MessageType.GiveawayCreated => $"{typeof(MessageType)}.{nameof(MessageType.GiveawayCreated)}",
                MessageType.Giveaway => $"{typeof(MessageType)}.{nameof(MessageType.Giveaway)}",
                MessageType.GiveawayWinners => $"{typeof(MessageType)}.{nameof(MessageType.GiveawayWinners)}",
                MessageType.GiveawayCompleted => $"{typeof(MessageType)}.{nameof(MessageType.GiveawayCompleted)}",
                MessageType.BoostAdded => $"{typeof(MessageType)}.{nameof(MessageType.BoostAdded)}",
                MessageType.ChatBackgroundSet => $"{typeof(MessageType)}.{nameof(MessageType.ChatBackgroundSet)}",
                MessageType.PaidMedia => $"{typeof(MessageType)}.{nameof(MessageType.PaidMedia)}",
                MessageType.RefundedPayment => $"{typeof(MessageType)}.{nameof(MessageType.RefundedPayment)}",
                _ => $"{typeof(MessageType)}.{nameof(MessageType.Unknown)}"
            },
            UpdateType.InlineQuery => $"{typeof(UpdateType)}.{nameof(UpdateType.InlineQuery)}",
            UpdateType.ChosenInlineResult => $"{typeof(UpdateType)}.{nameof(UpdateType.ChosenInlineResult)}",
            UpdateType.CallbackQuery => $"{typeof(UpdateType)}.{nameof(UpdateType.CallbackQuery)}",
            UpdateType.EditedMessage => $"{typeof(UpdateType)}.{nameof(UpdateType.EditedMessage)}",
            UpdateType.ChannelPost => $"{typeof(UpdateType)}.{nameof(UpdateType.ChannelPost)}",
            UpdateType.EditedChannelPost => $"{typeof(UpdateType)}.{nameof(UpdateType.EditedChannelPost)}",
            UpdateType.ShippingQuery => $"{typeof(UpdateType)}.{nameof(UpdateType.ShippingQuery)}",
            UpdateType.PreCheckoutQuery => $"{typeof(UpdateType)}.{nameof(UpdateType.PreCheckoutQuery)}",
            UpdateType.Poll => $"{typeof(UpdateType)}.{nameof(UpdateType.Poll)}",
            UpdateType.PollAnswer => $"{typeof(UpdateType)}.{nameof(UpdateType.PollAnswer)}",
            UpdateType.MyChatMember => $"{typeof(UpdateType)}.{nameof(UpdateType.MyChatMember)}",
            UpdateType.ChatMember => $"{typeof(UpdateType)}.{nameof(UpdateType.ChatMember)}",
            UpdateType.ChatJoinRequest => $"{typeof(UpdateType)}.{nameof(UpdateType.ChatJoinRequest)}",
            UpdateType.MessageReaction => $"{typeof(UpdateType)}.{nameof(UpdateType.MessageReaction)}",
            UpdateType.MessageReactionCount => $"{typeof(UpdateType)}.{nameof(UpdateType.MessageReactionCount)}",
            UpdateType.ChatBoost => $"{typeof(UpdateType)}.{nameof(UpdateType.ChatBoost)}",
            UpdateType.RemovedChatBoost => $"{typeof(UpdateType)}.{nameof(UpdateType.RemovedChatBoost)}",
            UpdateType.BusinessConnection => $"{typeof(UpdateType)}.{nameof(UpdateType.BusinessConnection)}",
            UpdateType.BusinessMessage => $"{typeof(UpdateType)}.{nameof(UpdateType.BusinessMessage)}",
            UpdateType.EditedBusinessMessage => $"{typeof(UpdateType)}.{nameof(UpdateType.EditedBusinessMessage)}",
            UpdateType.DeletedBusinessMessages => $"{typeof(UpdateType)}.{nameof(UpdateType.DeletedBusinessMessages)}",
            _ => $"{typeof(UpdateType)}.{nameof(UpdateType.Unknown)}"
        });
    }
}