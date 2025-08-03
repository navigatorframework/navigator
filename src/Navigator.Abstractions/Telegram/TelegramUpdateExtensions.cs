using Navigator.Abstractions.Entities;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Chat = Telegram.Bot.Types.Chat;
using User = Telegram.Bot.Types.User;

namespace Navigator.Abstractions.Telegram;

/// <summary>
///     Extensions for working with <see cref="Update" /> more fluently.
/// </summary>
public static class TelegramUpdateExtensions
{
    /// <summary>
    ///     Extracts the command name from a Telegram message that contains a bot command entity.
    ///     Removes the leading slash and optionally validates against a specific bot name.
    /// </summary>
    /// <param name="message">The Telegram message to extract the command from.</param>
    /// <param name="botName">
    ///     Optional bot name to validate against. If provided, only commands directed to this bot will be
    ///     returned.
    /// </param>
    /// <returns>The command name without the leading slash, or <c>null</c> if no valid command is found.</returns>
    public static string? ExtractCommand(this Message message, string? botName = default)
    {
        if (message.Entities?.First().Type != MessageEntityType.BotCommand) return default;

        var command = message.EntityValues?.First();

        command = command?[1..];

        if (command?.Contains('@') == false) return command;

        if (botName is not null && !command?.Contains(botName) == true) return default;

        command = command?[..command.IndexOf('@')];

        return command;
    }

    /// <summary>
    ///     Extracts command arguments from a Telegram message by parsing the text after the first space.
    ///     Used primarily for bot commands to get the parameters passed to the command.
    /// </summary>
    /// <param name="message">The Telegram message to extract arguments from.</param>
    /// <returns>An array of argument strings split by spaces, or an empty array if no arguments are found.</returns>
    public static string[] ExtractArguments(this Message message)
    {
        return message.Text is not null && message.Text.Contains(' ')
            ? message.Text.Remove(0, message.Text.IndexOf(' ') + 1).Split(' ')
            : [];
    }

    /// <summary>
    ///     Safely extracts the user information from various types of Telegram updates.
    ///     Supports multiple update types including messages, inline queries, callback queries, and more.
    /// </summary>
    /// <param name="update">The Telegram update to extract the user from.</param>
    /// <returns>The <see cref="User" /> if found, or <c>null</c> if the update type doesn't contain user information.</returns>
    public static User? GetUserOrDefault(this Update update)
    {
        return update.Type switch
        {
            UpdateType.Message => update.Message?.From,
            UpdateType.InlineQuery => update.InlineQuery?.From,
            UpdateType.ChosenInlineResult => update.ChosenInlineResult?.From,
            UpdateType.CallbackQuery => update.CallbackQuery?.From,
            UpdateType.EditedMessage => update.EditedMessage?.From,
            UpdateType.ChannelPost => update.ChannelPost?.From,
            UpdateType.EditedChannelPost => update.EditedChannelPost?.From,
            UpdateType.ShippingQuery => update.ShippingQuery?.From,
            UpdateType.PreCheckoutQuery => update.PreCheckoutQuery?.From,
            _ => default
        };
    }

    /// <summary>
    ///     Safely extracts the chat information from various types of Telegram updates.
    ///     Supports message-based updates including regular messages, edited messages, and channel posts.
    /// </summary>
    /// <param name="update">The Telegram update to extract the chat from.</param>
    /// <returns>The <see cref="Chat" /> if found, or <c>null</c> if the update type doesn't contain chat information.</returns>
    public static Chat? GetChatOrDefault(this Update update)
    {
        return update.Type switch
        {
            UpdateType.CallbackQuery => update.CallbackQuery?.Message?.Chat,
            UpdateType.Message => update.Message?.Chat,
            UpdateType.EditedMessage => update.EditedMessage?.Chat,
            UpdateType.ChannelPost => update.ChannelPost?.Chat,
            UpdateType.EditedChannelPost => update.EditedChannelPost?.Chat,
            _ => default
        };
    }

    /// <summary>
    ///     Creates a Navigator <see cref="Conversation" /> entity from a Telegram update by extracting and converting
    ///     the user and chat information to Navigator's internal entity types.
    /// </summary>
    /// <param name="update">The Telegram update to create the conversation from.</param>
    /// <returns>A <see cref="Conversation" /> containing the converted user and optional chat information.</returns>
    /// <exception cref="NavigatorException">Thrown when no user information can be extracted from the update.</exception>
    public static Conversation GetConversation(this Update update)
    {
        var rawUser = update.GetUserOrDefault();
        var rawChat = update.GetChatOrDefault();

        if (rawUser is null) throw new NavigatorException("No conversation could be built, user not found.");

        var user = rawUser.IsBot
            ? new Bot(rawUser.Id, rawUser.FirstName)
            {
                Username = rawUser.Username!,
                LastName = rawUser.LastName,
                LanguageCode = rawUser.LanguageCode,
                CanJoinGroups = rawUser.CanJoinGroups,
                CanReadAllGroupMessages = rawUser.CanReadAllGroupMessages,
                SupportsInlineQueries = rawUser.SupportsInlineQueries
            }
            : new Abstractions.Entities.User(rawUser.Id, rawUser.FirstName)
            {
                Username = rawUser.Username,
                LastName = rawUser.LastName,
                LanguageCode = rawUser.LanguageCode,
                IsPremium = rawUser.IsPremium,
                HasBotInAttachmentMenu = rawUser.AddedToAttachmentMenu
            };

        var chat = default(Abstractions.Entities.Chat);

        if (rawChat is not null)
            chat = new Abstractions.Entities.Chat(rawChat.Id, (Abstractions.Entities.Chat.ChatType)rawChat.Type)
            {
                Title = rawChat.Title,
                IsForum = rawChat.IsForum
            };

        return new Conversation(user)
        {
            Chat = chat
        };
    }
}