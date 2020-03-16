﻿using System.Linq;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Navigator.Extensions.Actions.Extensions
{
    public static class MessageExtension
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
    }
}