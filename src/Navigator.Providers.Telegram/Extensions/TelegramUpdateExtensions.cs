using System;
using System.Linq;
using Navigator.Providers.Telegram.Entities;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Chat = Telegram.Bot.Types.Chat;
using User = Telegram.Bot.Types.User;

namespace Navigator.Providers.Telegram.Extensions
{
    internal static class TelegramUpdateExtensions
    {
        public static string? ExtractCommand(this Message message, string? botName)
        {
            if (message.Entities?.First().Type != MessageEntityType.BotCommand) return default;

            var command = message.EntityValues?.First();

            if (command?.Contains('@') == false) return command;

            if (botName is not null && !command?.Contains(botName) == false) return default;

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
                UpdateType.Message => update.Message.From,
                UpdateType.InlineQuery => update.InlineQuery.From,
                UpdateType.ChosenInlineResult => update.ChosenInlineResult.From,
                UpdateType.CallbackQuery => update.CallbackQuery.From,
                UpdateType.EditedMessage => update.EditedMessage.From,
                UpdateType.ChannelPost => update.ChannelPost.From,
                UpdateType.EditedChannelPost => update.EditedChannelPost.From,
                UpdateType.ShippingQuery => update.ShippingQuery.From,
                UpdateType.PreCheckoutQuery => update.PreCheckoutQuery.From,
                _ => default
            };
        }

        public static Chat? GetChatOrDefault(this Update update)
        {
            return update.Type switch
            {
                UpdateType.CallbackQuery => update.CallbackQuery.Message.Chat,
                UpdateType.Message => update.Message.Chat,
                UpdateType.EditedMessage => update.EditedMessage.Chat,
                UpdateType.ChannelPost => update.ChannelPost.Chat,
                UpdateType.EditedChannelPost => update.EditedChannelPost.Chat,
                _ => default
            };
        }

        public static TelegramConversation GetConversation(this Update update)
        {
            var user = update.GetUserOrDefault();
            var chat = update.GetChatOrDefault();

            if (chat is null || user is null)
            {
                throw new Exception("TODO NAvigator exception no conversation could be built.");
            }

            return new TelegramConversation
            {
                User = new TelegramUser(user.Id)
                {
                    Username = user.Username
                },
                Chat = new TelegramChat(chat.Id)
                {
                    Title = chat.Title
                }
            };
        }
    }
}