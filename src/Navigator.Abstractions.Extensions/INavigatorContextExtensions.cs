using System;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.Payments;

namespace Navigator.Abstractions.Extensions
{
    /// <summary>
    /// Useful extensions for Navigator Context.
    /// </summary>
    public static class INavigatorContextExtensions
    {
        #region User

        /// <summary>
        /// Get a Telegram user.
        /// </summary>
        /// <param name="ctx"></param>
        /// <exception cref="Exception">When user is not found.</exception>
        /// <returns></returns>
        public static User GetTelegramUser(this INavigatorContext ctx)
        {
            var user = ctx.GetTelegramUserOrDefault();

            return user ?? throw new Exception("User not found in update.");
        }

        /// <summary>
        /// Get a Telegram user or default if not found.
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Get a Telegram chat.
        /// </summary>
        /// <param name="ctx"></param>
        /// <exception cref="Exception">When chat is not found.</exception>
        /// <returns></returns>
        public static Chat GetTelegramChat(this INavigatorContext ctx)
        {
            var chat = ctx.GetTelegramChatOrDefault();

            return chat ?? throw new Exception("Chat not found in update.");
        }

        /// <summary>
        /// Get a Telegram chat or default if not found.
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns>Telegram Chat</returns>
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

        /// <summary>
        /// Get a Telegram message.
        /// </summary>
        /// <param name="ctx"></param>
        /// <exception cref="Exception">When message is not found.</exception>
        /// <returns></returns>
        public static Message GetMessage(this INavigatorContext ctx)
        {
            return ctx.GetMessageOrDefault() ?? throw new Exception("Message not found in update.");
        }

        /// <summary>
        /// Get a Telegram message or default if not found.
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public static Message? GetMessageOrDefault(this INavigatorContext ctx)
        {
            return ctx.Update.Type == UpdateType.Message ? ctx.Update.Message : default;
        }

        #endregion

        #region InlineQuery

        /// <summary>
        /// Get a Telegram inline query
        /// </summary>
        /// <param name="ctx"></param>
        /// <exception cref="Exception">When inline query is not found.</exception>
        /// <returns></returns>
        public static InlineQuery GetInlineQuery(this INavigatorContext ctx)
        {
            return ctx.GetInlineQueryOrDefault() ?? throw new Exception("InlineQuery not found in update.");
        }

        /// <summary>
        /// Get a Telegram inline query or default if not found.
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public static InlineQuery? GetInlineQueryOrDefault(this INavigatorContext ctx)
        {
            return ctx.Update.Type == UpdateType.InlineQuery ? ctx.Update.InlineQuery : default;
        }

        #endregion

        #region ChosenInlineResult

        public static ChosenInlineResult GetChosenInlineResult(this INavigatorContext ctx)
        {
            return ctx.GetChosenInlineResultOrDefault() ?? throw new Exception("ChosenInlineResult not found in update.");
        }

        public static ChosenInlineResult? GetChosenInlineResultOrDefault(this INavigatorContext ctx)
        {
            return ctx.Update.Type == UpdateType.ChosenInlineResult ? ctx.Update.ChosenInlineResult : default;
        }

        #endregion

        #region CallbackQuery

        public static CallbackQuery GetCallbackQuery(this INavigatorContext ctx)
        {
            return ctx.GetCallbackQueryOrDefault() ?? throw new Exception("CallbackQuery not found in update.");
        }

        public static CallbackQuery? GetCallbackQueryOrDefault(this INavigatorContext ctx)
        {
            return ctx.Update.Type == UpdateType.CallbackQuery ? ctx.Update.CallbackQuery : default;
        }

        #endregion

        #region EditedMessage

        public static Message GetEditedMessage(this INavigatorContext ctx)
        {
            return ctx.GetEditedMessageOrDefault() ?? throw new Exception("EditedMessage not found in update.");
        }

        public static Message? GetEditedMessageOrDefault(this INavigatorContext ctx)
        {
            return ctx.Update.Type == UpdateType.EditedMessage ? ctx.Update.EditedMessage : default;
        }

        #endregion

        #region ChannelPost

        public static Message GetChannelPost(this INavigatorContext ctx)
        {
            return ctx.GetChannelPostOrDefault() ?? throw new Exception("ChannelPost not found in update.");
        }

        public static Message? GetChannelPostOrDefault(this INavigatorContext ctx)
        {
            return ctx.Update.Type == UpdateType.ChannelPost ? ctx.Update.ChannelPost : default;
        }

        #endregion

        #region EditedChannelPost

        public static Message GetEditedChannelPost(this INavigatorContext ctx)
        {
            return ctx.GetEditedChannelPostOrDefault() ?? throw new Exception("EditedChannelPost not found in update.");
        }

        public static Message? GetEditedChannelPostOrDefault(this INavigatorContext ctx)
        {
            return ctx.Update.Type == UpdateType.EditedChannelPost ? ctx.Update.EditedChannelPost : default;
        }

        #endregion

        #region ShippingQuery

        public static ShippingQuery GetShippingQuery(this INavigatorContext ctx)
        {
            return ctx.GetShippingQueryOrDefault() ?? throw new Exception("ShippingQuery not found in update.");
        }

        public static ShippingQuery? GetShippingQueryOrDefault(this INavigatorContext ctx)
        {
            return ctx.Update.Type == UpdateType.ShippingQuery ? ctx.Update.ShippingQuery : default;
        }

        #endregion

        #region PreCheckoutQuery

        public static PreCheckoutQuery GetPreCheckoutQuery(this INavigatorContext ctx)
        {
            return ctx.GetPreCheckoutQueryOrDefault() ?? throw new Exception("PreCheckoutQuery not found in update.");
        }

        public static PreCheckoutQuery? GetPreCheckoutQueryOrDefault(this INavigatorContext ctx)
        {
            return ctx.Update.Type == UpdateType.PreCheckoutQuery ? ctx.Update.PreCheckoutQuery : default;
        }

        #endregion

        #region Poll

        public static Poll GetPoll(this INavigatorContext ctx)
        {
            return ctx.GetPollOrDefault() ?? throw new Exception("Poll not found in update.");
        }

        public static Poll? GetPollOrDefault(this INavigatorContext ctx)
        {
            return ctx.Update.Type == UpdateType.Poll ? ctx.Update.Poll : default;
        }

        #endregion
    }
}