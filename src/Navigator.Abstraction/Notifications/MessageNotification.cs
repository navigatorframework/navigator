using System.Collections.Generic;
using MediatR;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Navigator.Abstraction.Notifications
{
    public class MessageNotification : Message, INotification
    {
        public new IEnumerable<string> EntityValues { get; set; }
        public new IEnumerable<string> CaptionEntityValues { get; set; }
        public new MessageType Type { get; set; }
    }
}