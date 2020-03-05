﻿using MediatR;
using Telegram.Bot.Types;

namespace Navigator.Core.Abstractions.Notifications
{
    public class CallbackQueryNotification : CallbackQuery, INotification
    {
        public new bool IsGameQuery { get; set; }
    }
}