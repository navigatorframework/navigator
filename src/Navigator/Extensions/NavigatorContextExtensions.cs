using System;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.Payments;

namespace Navigator.Extensions
{
    public static class NavigatorContextExtensions
    {
        #region User

        public static User GetTelegramUser(this NavigatorContext ctx)
        {
            var user = ctx.GetTelegramUserOrDefault();

            return user ?? throw new Exception("User not found in update.");
        }

        public static User? GetTelegramUserOrDefault(this NavigatorContext ctx)
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

        public static Chat GetTelegramChat(this NavigatorContext ctx)
        {
            var chat = ctx.GetTelegramChatOrDefault();

            return chat ?? throw new Exception("Chat not found in update.");
        }

        public static Chat? GetTelegramChatOrDefault(this NavigatorContext ctx)
        {
            return ctx.Update.Type switch
            {
                UpdateType.Message => ctx.Update.Message.Chat,
                UpdateType.EditedMessage => ctx.Update.EditedMessage.Chat,
                UpdateType.ChannelPost => ctx.Update.ChannelPost.Chat,
                UpdateType.EditedChannelPost => ctx.Update.EditedChannelPost.Chat,
                _ => default
            };
        }

        #endregion

        #region Message

        public static Message GetMessage(this NavigatorContext ctx)
        {
            var message = ctx.GetMessageOrDefault();

            return message ?? throw new Exception("Message not found in update.");
        }

        public static Message? GetMessageOrDefault(this NavigatorContext ctx, Message defaultValue = default)
        {
            return ctx.Update.Type == UpdateType.Message ? ctx.Update.Message : defaultValue;
        }

        #endregion

        #region InlineQuery

        public static InlineQuery GetInlineQuery(this NavigatorContext ctx)
        {
            var inlineQuery = ctx.GetInlineQueryOrDefault();

            return inlineQuery ?? throw new Exception("InlineQuery not found in update.");
        }

        public static InlineQuery? GetInlineQueryOrDefault(this NavigatorContext ctx, InlineQuery defaultValue = default)
        {
            return ctx.Update.Type == UpdateType.InlineQuery ? ctx.Update.InlineQuery : defaultValue;
        }

        #endregion

        #region ChosenInlineResult

        public static ChosenInlineResult GetChosenInlineResult(this NavigatorContext ctx)
        {
            var chosenInlineResult = ctx.GetChosenInlineResultOrDefault();

            return chosenInlineResult ?? throw new Exception("ChosenInlineResult not found in update.");
        }

        public static ChosenInlineResult? GetChosenInlineResultOrDefault(this NavigatorContext ctx, ChosenInlineResult defaultValue = default)
        {
            return ctx.Update.Type == UpdateType.ChosenInlineResult ? ctx.Update.ChosenInlineResult : defaultValue;
        }

        #endregion

        #region CallbackQuery

        public static CallbackQuery GetCallbackQuery(this NavigatorContext ctx)
        {
            var chosenInlineResult = ctx.GetCallbackQueryOrDefault();

            return chosenInlineResult ?? throw new Exception("CallbackQuery not found in update.");
        }

        public static CallbackQuery? GetCallbackQueryOrDefault(this NavigatorContext ctx, CallbackQuery defaultValue = default)
        {
            return ctx.Update.Type == UpdateType.CallbackQuery ? ctx.Update.CallbackQuery : defaultValue;
        }

        #endregion

        #region EditedMessage

        public static Message GetEditedMessage(this NavigatorContext ctx)
        {
            var editedMessage = ctx.GetEditedMessageOrDefault();

            return editedMessage ?? throw new Exception("EditedMessage not found in update.");
        }

        public static Message? GetEditedMessageOrDefault(this NavigatorContext ctx, Message defaultValue = default)
        {
            return ctx.Update.Type == UpdateType.EditedMessage ? ctx.Update.EditedMessage : defaultValue;
        }

        #endregion

        #region ChannelPost

        public static Message GetChannelPost(this NavigatorContext ctx)
        {
            var channelPost = ctx.GetChannelPostOrDefault();

            return channelPost ?? throw new Exception("ChannelPost not found in update.");
        }

        public static Message? GetChannelPostOrDefault(this NavigatorContext ctx, Message defaultValue = default)
        {
            return ctx.Update.Type == UpdateType.ChannelPost ? ctx.Update.ChannelPost : defaultValue;
        }

        #endregion

        #region EditedChannelPost

        public static Message GetEditedChannelPost(this NavigatorContext ctx)
        {
            var editedChannelPost = ctx.GetEditedChannelPostOrDefault();

            return editedChannelPost ?? throw new Exception("EditedChannelPost not found in update.");
        }

        public static Message? GetEditedChannelPostOrDefault(this NavigatorContext ctx, Message defaultValue = default)
        {
            return ctx.Update.Type == UpdateType.EditedChannelPost ? ctx.Update.EditedChannelPost : defaultValue;
        }

        #endregion

        #region ShippingQuery

        public static ShippingQuery GetShippingQuery(this NavigatorContext ctx)
        {
            var shippingQuery = ctx.GetShippingQueryOrDefault();

            return shippingQuery ?? throw new Exception("ShippingQuery not found in update.");
        }

        public static ShippingQuery? GetShippingQueryOrDefault(this NavigatorContext ctx, ShippingQuery defaultValue = default)
        {
            return ctx.Update.Type == UpdateType.ShippingQuery ? ctx.Update.ShippingQuery : defaultValue;
        }

        #endregion

        #region PreCheckoutQuery

        public static PreCheckoutQuery GetPreCheckoutQuery(this NavigatorContext ctx)
        {
            var preCheckoutQuery = ctx.GetPreCheckoutQueryOrDefault();

            return preCheckoutQuery ?? throw new Exception("PreCheckoutQuery not found in update.");
        }

        public static PreCheckoutQuery? GetPreCheckoutQueryOrDefault(this NavigatorContext ctx, PreCheckoutQuery defaultValue = default)
        {
            return ctx.Update.Type == UpdateType.PreCheckoutQuery ? ctx.Update.PreCheckoutQuery : defaultValue;
        }

        #endregion

        #region Poll

        public static Poll GetPoll(this NavigatorContext ctx)
        {
            var poll = ctx.GetPollOrDefault();

            return poll ?? throw new Exception("Poll not found in update.");
        }

        public static Poll? GetPollOrDefault(this NavigatorContext ctx, Poll defaultValue = default)
        {
            return ctx.Update.Type == UpdateType.Poll ? ctx.Update.Poll : defaultValue;
        }

        #endregion
    }
}