using Navigator.Abstractions.Actions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.Payments;

namespace Navigator.Strategy.TypeProvider;

internal sealed record TelegramUpdateTypeProvider : IArgumentTypeProvider
{
    /// <inheritdoc />
    public ushort Priority => 10000;

    /// <inheritdoc />
    public ValueTask<object?> GetArgument(Type type, Update update, BotAction action)
    {
        return ValueTask.FromResult<object?>(type switch
        {
            not null when type == typeof(Message) && update.Type == UpdateType.Message
                => update.Message,
            not null when type == typeof(InlineQuery) && update.Type == UpdateType.InlineQuery
                => update.InlineQuery,
            not null when type == typeof(ChosenInlineResult) && update.Type == UpdateType.ChosenInlineResult
                => update.ChosenInlineResult,
            not null when type == typeof(CallbackQuery) && update.Type == UpdateType.CallbackQuery
                => update.CallbackQuery,
            not null when type == typeof(Message) && update.Type == UpdateType.EditedMessage
                => update.EditedMessage,
            not null when type == typeof(Message) && update.Type == UpdateType.ChannelPost
                => update.ChannelPost,
            not null when type == typeof(Message) && update.Type == UpdateType.EditedChannelPost
                => update.EditedChannelPost,
            not null when type == typeof(ShippingQuery) && update.Type == UpdateType.ShippingQuery
                => update.ShippingQuery,
            not null when type == typeof(PreCheckoutQuery) && update.Type == UpdateType.PreCheckoutQuery
                => update.PreCheckoutQuery,
            not null when type == typeof(Poll) && update.Type == UpdateType.Poll
                => update.Poll,
            not null when type == typeof(PollAnswer) && update.Type == UpdateType.PollAnswer
                => update.PollAnswer,
            not null when type == typeof(ChatMemberUpdated) && update.Type == UpdateType.MyChatMember
                => update.MyChatMember,
            not null when type == typeof(ChatMember) && update.Type == UpdateType.ChatMember
                => update.ChatMember,
            not null when type == typeof(ChatJoinRequest) && update.Type == UpdateType.ChatJoinRequest
                => update.ChatJoinRequest,
            not null when type == typeof(MessageReactionUpdated) && update.Type == UpdateType.MessageReaction
                => update.MessageReaction,
            not null when type == typeof(MessageReactionCountUpdated) && update.Type == UpdateType.MessageReactionCount
                => update.MessageReactionCount,
            not null when type == typeof(ChatBoostRemoved) && update.Type == UpdateType.RemovedChatBoost
                => update.RemovedChatBoost,
            not null when type == typeof(BusinessConnection) && update.Type == UpdateType.BusinessConnection
                => update.BusinessConnection,
            not null when type == typeof(Message) && update.Type == UpdateType.BusinessMessage
                => update.BusinessMessage,
            not null when type == typeof(Message) && update.Type == UpdateType.EditedBusinessMessage
                => update.EditedBusinessMessage,
            not null when type == typeof(Message) && update.Type == UpdateType.DeletedBusinessMessages
                => update.DeletedBusinessMessages,
            _ => default
        });
    }
}