using System;
using Navigator.Abstraction;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.Payments;

namespace Navigator
{
    public static class NavigatorContextExtensions
    {
        #region User

        public static User GetTelegramUser(this INavigatorContext ctx)
        {
            var user = ctx.GetTelegramUserOrDefault();

            return user ?? throw new Exception("User not found in update.");
        }

        public static User? GetTelegramUserOrDefault(this INavigatorContext ctx)
        {
            return ctx.Update.Type switch
            {
                UpdateType.Message => ctx.Update.Message.From,
                UpdateType.InlineQuery => ctx.Update.InlineQuery.From,
                UpdateType.ChosenInlineResult => ctx.Update.ChosenInlineResult.From,
                UpdateType.CallbackQuery => ctx.Update.CallbackQuery.From,
                UpdateType.EditedMessage => ctx.Update.EditedMessage.From,
                UpdateType.ChannelPost => ctx.Update.ChannelPost.From,
                UpdateType.EditedChannelPost => ctx.Update.EditedChannelPost.From,
                UpdateType.ShippingQuery => ctx.Update.ShippingQuery.From,
                UpdateType.PreCheckoutQuery => ctx.Update.PreCheckoutQuery.From,
                _ => default
            };
        }

        #endregion

        #region Chat

        public static Chat GetTelegramChat(this INavigatorContext ctx)
        {
            var chat = ctx.GetTelegramChatOrDefault();

            return chat ?? throw new Exception("Chat not found in update.");
        }

        public static Chat? GetTelegramChatOrDefault(this INavigatorContext ctx)
        {
            return ctx.Update.Type switch
            {
                UpdateType.CallbackQuery => ctx.Update.CallbackQuery.Message.Chat,
                UpdateType.Message => ctx.Update.Message.Chat,
                UpdateType.EditedMessage => ctx.Update.EditedMessage.Chat,
                UpdateType.ChannelPost => ctx.Update.ChannelPost.Chat,
                UpdateType.EditedChannelPost => ctx.Update.EditedChannelPost.Chat,
                _ => default
            };
        }

        #endregion

        #region Message

        public static Message GetMessage(this INavigatorContext ctx)
        {
            return ctx.GetMessageOrDefault() ?? throw new Exception("Message not found in update.");
        }

        public static Message? GetMessageOrDefault(this INavigatorContext ctx, Message defaultValue = default)
        {
            return ctx.Update.Type == UpdateType.Message ? ctx.Update.Message : defaultValue;
        }

        #endregion

        #region InlineQuery

        public static InlineQuery GetInlineQuery(this INavigatorContext ctx)
        {
            return ctx.GetInlineQueryOrDefault() ?? throw new Exception("InlineQuery not found in update.");
        }

        public static InlineQuery? GetInlineQueryOrDefault(this INavigatorContext ctx, InlineQuery defaultValue = default)
        {
            return ctx.Update.Type == UpdateType.InlineQuery ? ctx.Update.InlineQuery : defaultValue;
        }

        #endregion

        #region ChosenInlineResult

        public static ChosenInlineResult GetChosenInlineResult(this INavigatorContext ctx)
        {
            return ctx.GetChosenInlineResultOrDefault() ?? throw new Exception("ChosenInlineResult not found in update.");
        }

        public static ChosenInlineResult? GetChosenInlineResultOrDefault(this INavigatorContext ctx, ChosenInlineResult defaultValue = default)
        {
            return ctx.Update.Type == UpdateType.ChosenInlineResult ? ctx.Update.ChosenInlineResult : defaultValue;
        }

        #endregion

        #region CallbackQuery

        public static CallbackQuery GetCallbackQuery(this INavigatorContext ctx)
        {
            return ctx.GetCallbackQueryOrDefault() ?? throw new Exception("CallbackQuery not found in update.");
        }

        public static CallbackQuery? GetCallbackQueryOrDefault(this INavigatorContext ctx, CallbackQuery defaultValue = default)
        {
            return ctx.Update.Type == UpdateType.CallbackQuery ? ctx.Update.CallbackQuery : defaultValue;
        }

        #endregion

        #region EditedMessage

        public static Message GetEditedMessage(this INavigatorContext ctx)
        {
            return ctx.GetEditedMessageOrDefault() ?? throw new Exception("EditedMessage not found in update.");
        }

        public static Message? GetEditedMessageOrDefault(this INavigatorContext ctx, Message defaultValue = default)
        {
            return ctx.Update.Type == UpdateType.EditedMessage ? ctx.Update.EditedMessage : defaultValue;
        }

        #endregion

        #region ChannelPost

        public static Message GetChannelPost(this INavigatorContext ctx)
        {
            return ctx.GetChannelPostOrDefault() ?? throw new Exception("ChannelPost not found in update.");
        }

        public static Message? GetChannelPostOrDefault(this INavigatorContext ctx, Message defaultValue = default)
        {
            return ctx.Update.Type == UpdateType.ChannelPost ? ctx.Update.ChannelPost : defaultValue;
        }

        #endregion

        #region EditedChannelPost

        public static Message GetEditedChannelPost(this INavigatorContext ctx)
        {
            return ctx.GetEditedChannelPostOrDefault() ?? throw new Exception("EditedChannelPost not found in update.");
        }

        public static Message? GetEditedChannelPostOrDefault(this INavigatorContext ctx, Message defaultValue = default)
        {
            return ctx.Update.Type == UpdateType.EditedChannelPost ? ctx.Update.EditedChannelPost : defaultValue;
        }

        #endregion

        #region ShippingQuery

        public static ShippingQuery GetShippingQuery(this INavigatorContext ctx)
        {
            return ctx.GetShippingQueryOrDefault() ?? throw new Exception("ShippingQuery not found in update.");
        }

        public static ShippingQuery? GetShippingQueryOrDefault(this INavigatorContext ctx, ShippingQuery defaultValue = default)
        {
            return ctx.Update.Type == UpdateType.ShippingQuery ? ctx.Update.ShippingQuery : defaultValue;
        }

        #endregion

        #region PreCheckoutQuery

        public static PreCheckoutQuery GetPreCheckoutQuery(this INavigatorContext ctx)
        {
            return ctx.GetPreCheckoutQueryOrDefault() ?? throw new Exception("PreCheckoutQuery not found in update.");
        }

        public static PreCheckoutQuery? GetPreCheckoutQueryOrDefault(this INavigatorContext ctx, PreCheckoutQuery defaultValue = default)
        {
            return ctx.Update.Type == UpdateType.PreCheckoutQuery ? ctx.Update.PreCheckoutQuery : defaultValue;
        }

        #endregion

        #region Poll

        public static Poll GetPoll(this INavigatorContext ctx)
        {
            return ctx.GetPollOrDefault() ?? throw new Exception("Poll not found in update.");
        }

        public static Poll? GetPollOrDefault(this INavigatorContext ctx, Poll defaultValue = default)
        {
            return ctx.Update.Type == UpdateType.Poll ? ctx.Update.Poll : defaultValue;
        }

        #endregion
    }
}