using System;
using Navigator.Abstraction;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Payments;

namespace Navigator.Notification
{
    public abstract class NavigatorNotification : INavigatorNotification
    {
        public DateTime Timestamp { get; }
        public int UpdateId { get; }

        protected NavigatorNotification(Update update)
        {
            UpdateId = update.Id;
            Timestamp = DateTime.UtcNow;
        }
    }

    public class MessageNotification : NavigatorNotification
    {
        public readonly Message Message;

        public MessageNotification(Update update) : base(update)
        {
            Message = update.Message;
        }
    }

    public class InlineQueryNotification : NavigatorNotification
    {
        public readonly InlineQuery InlineQuery;

        public InlineQueryNotification(Update update) : base(update)
        {
            InlineQuery = update.InlineQuery;
        }
    }

    public class ChosenInlineResultNotification : NavigatorNotification
    {
        public readonly ChosenInlineResult ChosenInlineResult;
        
        public ChosenInlineResultNotification(Update update) : base(update)
        {
            ChosenInlineResult = update.ChosenInlineResult;
        }
    }

    public class CallbackQueryNotification : NavigatorNotification
    {
        public readonly CallbackQuery CallbackQuery;
        
        public CallbackQueryNotification(Update update) : base(update)
        {
            CallbackQuery = update.CallbackQuery;
        }
    }

    public class EditedMessageNotification : NavigatorNotification
    {
        public readonly Message EditedMessage;
        
        public EditedMessageNotification(Update update) : base(update)
        {
            EditedMessage = update.EditedMessage;
        }
    }

    public class ChannelPostNotification : NavigatorNotification
    {
        public readonly Message ChannelPost;
        
        public ChannelPostNotification(Update update) : base(update)
        {
            ChannelPost = update.ChannelPost;
        }
    }

    public class EditedChannelPostNotification : NavigatorNotification
    {
        public readonly Message EditedChannelPost;

        public EditedChannelPostNotification(Update update) : base(update)
        {
            EditedChannelPost = update.EditedChannelPost;
        }
    }

    public class ShippingQueryNotification : NavigatorNotification
    {
        public readonly ShippingQuery ShippingQuery;
        
        public ShippingQueryNotification(Update update) : base(update)
        {
            ShippingQuery = update.ShippingQuery;
        }
    }

    public class PreCheckoutQueryNotification : NavigatorNotification
    {
        public readonly PreCheckoutQuery PreCheckoutQuery;
        
        public PreCheckoutQueryNotification(Update update) : base(update)
        {
            PreCheckoutQuery = update.PreCheckoutQuery;
        }
    }

    public class PollNotification : NavigatorNotification
    {
        public readonly Poll Poll;
        
        public PollNotification(Update update) : base(update)
        {
            Poll = update.Poll;
        }
    }
}