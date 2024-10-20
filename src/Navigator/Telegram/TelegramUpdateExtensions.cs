using Navigator.Abstractions;
using Navigator.Abstractions.Entities;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Chat = Telegram.Bot.Types.Chat;
using User = Telegram.Bot.Types.User;

namespace Navigator.Telegram;

internal static class TelegramUpdateExtensions
{
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

    public static string[] ExtractArguments(this Message message)
    {
        return message.Text is not null && message.Text.Contains(' ')
            ? message.Text.Remove(0, message.Text.IndexOf(' ') + 1).Split(' ')
            : [];
    }

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