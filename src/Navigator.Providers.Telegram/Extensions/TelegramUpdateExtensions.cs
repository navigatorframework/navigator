using System.Linq;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Navigator.Providers.Telegram.Extensions
{
    internal static class TelegramUpdateExtensions
    {
        public static string? ExtractCommand(this Message message, string botName)
        {
            if (message.Entities?.First()?.Type != MessageEntityType.BotCommand) return default;

            var command = message.EntityValues.First();

            if (!command.Contains('@')) return command;

            if (!command.Contains(botName)) return default;

            command = command.Substring(0, command.IndexOf('@'));

            return command;
        }
        
        public static string ExtractArguments(this Message message, string botName)
        {
            return message.Text.Contains(' ') 
                ? message.Text.Remove(0, message.Text.IndexOf(' ') + 1) 
                : string.Empty;
        }
    }
}