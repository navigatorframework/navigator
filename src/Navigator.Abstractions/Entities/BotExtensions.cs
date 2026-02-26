using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Navigator.Abstractions.Entities;

/// <summary>
///     Extensions for <see cref="Bot" /> to detect replies and mentions in Telegram messages and updates.
/// </summary>
public static class BotExtensions
{
    /// <summary>
    ///     Provides instance extensions for detecting bot interactions in messages and updates.
    /// </summary>
    /// <param name="bot">The bot to check against.</param>
    extension(Bot bot)
    {
        /// <summary>
        ///     Determines whether the bot is being directly replied to in the given message.
        /// </summary>
        /// <param name="message">The Telegram message to check.</param>
        /// <returns><c>true</c> if the message is a reply to a message sent by this bot; otherwise, <c>false</c>.</returns>
        public bool IsRepliedTo(Message message)
            => message.ReplyToMessage?.From?.Id == bot.Id;

        /// <summary>
        ///     Determines whether the bot is being directly replied to in the given update.
        /// </summary>
        /// <param name="update">The Telegram update to check.</param>
        /// <returns><c>true</c> if the update contains a message that replies to this bot; otherwise, <c>false</c>.</returns>
        public bool IsRepliedTo(Update update)
        {
            var message = GetMessageFromUpdate(update);
            return message is not null && bot.IsRepliedTo(message);
        }

        /// <summary>
        ///     Determines whether the bot is mentioned in the given message,
        ///     either via an <c>@username</c> entity or by its first name appearing in the text.
        /// </summary>
        /// <param name="message">The Telegram message to check.</param>
        /// <returns><c>true</c> if the bot is mentioned; otherwise, <c>false</c>.</returns>
        public bool IsMentioned(Message message)
        {
            if (HasMentionEntity(bot, message.Text, message.Entities))
                return true;

            if (HasMentionEntity(bot, message.Caption, message.CaptionEntities))
                return true;

            var text = message.Text ?? message.Caption;
            return text?.Contains(bot.FirstName, StringComparison.OrdinalIgnoreCase) == true;
        }

        /// <summary>
        ///     Determines whether the bot is mentioned in the given update,
        ///     either via an <c>@username</c> entity or by its first name appearing in the text.
        /// </summary>
        /// <param name="update">The Telegram update to check.</param>
        /// <returns><c>true</c> if the update contains a message mentioning this bot; otherwise, <c>false</c>.</returns>
        public bool IsMentioned(Update update)
        {
            var message = GetMessageFromUpdate(update);
            return message is not null && bot.IsMentioned(message);
        }
    }

    private static bool HasMentionEntity(Bot bot, string? text, MessageEntity[]? entities)
    {
        if (text is null || entities is null) return false;

        foreach (var entity in entities)
        {
            switch (entity.Type)
            {
                case MessageEntityType.Mention when bot.Username is not null:
                {
                    var mention = text.Substring(entity.Offset, entity.Length);
                    if (mention.Equals($"@{bot.Username}", StringComparison.OrdinalIgnoreCase))
                        return true;
                    break;
                }
                case MessageEntityType.TextMention when entity.User?.Id == bot.Id:
                    return true;
            }
        }

        return false;
    }

    private static Message? GetMessageFromUpdate(Update update)
    {
        return update.Type switch
        {
            UpdateType.Message => update.Message,
            UpdateType.EditedMessage => update.EditedMessage,
            UpdateType.ChannelPost => update.ChannelPost,
            UpdateType.EditedChannelPost => update.EditedChannelPost,
            UpdateType.CallbackQuery => update.CallbackQuery?.Message as Message,
            _ => null
        };
    }
}
