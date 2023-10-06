using Navigator.Entities;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Chat = Telegram.Bot.Types.Chat;
using User = Telegram.Bot.Types.User;

namespace Navigator.Telegram;

internal static class TelegramUpdateExtensions
{
    public static string? ExtractCommand(this Message message, string? botName)
    {
        if (message.Entities?.First().Type != MessageEntityType.BotCommand) return default;

        var command = message.EntityValues?.First();

        if (command?.Contains('@') == false) return command;

        if (botName is not null && !command?.Contains(botName) == true) return default;

        command = command?[..command.IndexOf('@')];

        return command;
    }

    public static string? ExtractArguments(this Message message)
    {
        return message.Text is not null && message.Text.Contains(' ')
            ? message.Text.Remove(0, message.Text.IndexOf(' ') + 1)
            : default;
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

        if (rawUser is null)
        {
            throw new NavigatorException("No conversation could be built, user not found.");
        }

        var user = new Entities.User
        {
            Username = rawUser.Username,
            FirstName = rawUser.FirstName,
            LastName = rawUser.LastName,
            LanguageCode = rawUser.LanguageCode
        };

        var chat = default(Entities.Chat);

        if (rawChat is not null)
        {
            chat = new Entities.Chat
            {
                Id = rawChat.Id,
                Title = rawChat.Title,
                Type = (Entities.ChatType) rawChat.Type
            };
        }

        return new Conversation(user, chat);
    }
}