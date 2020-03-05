﻿using MediatR;
using Telegram.Bot.Types;
using UpdateType = Telegram.Bot.Types.Enums.UpdateType;

namespace Navigator.Core.Abstractions.Notifications
{
    public class UpdateNotification : Update, INotification
    {
        public new UpdateType Type { get; set; }
    }
}