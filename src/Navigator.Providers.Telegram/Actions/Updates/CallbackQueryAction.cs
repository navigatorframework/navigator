using Navigator.Actions;
using Navigator.Context;
using Navigator.Context.Extensions;
using Telegram.Bot.Types;

namespace Navigator.Providers.Telegram.Actions.Updates
{
    /// <summary>
    /// A callback query based action.
    /// </summary>
    public abstract class CallbackQueryAction : BaseAction
    {
        /// <summary>
        /// The original <see cref="Update.CallbackQuery"/>
        /// </summary>
        public CallbackQuery CallbackQuery { get; protected set; }

        /// <summary>
        /// The message that originated the callback query. Iy may be null if the message is too old.
        /// </summary>
        public Message? OriginalMessage { get; protected set; }

        /// <summary>
        /// Any data present on the callback query.
        /// </summary>
        public string? Data { get; protected set; }

        /// <summary>
        /// True if the callback query is from a game.
        /// </summary>
        public bool IsGameQuery { get; protected set; }

        /// <inheritdoc />
        protected CallbackQueryAction(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
        {
            var update = NavigatorContextAccessor.NavigatorContext.GetOriginalEvent<Update>();

            CallbackQuery = update.CallbackQuery;
            OriginalMessage = update.CallbackQuery.Message;
            Data = string.IsNullOrWhiteSpace(update.CallbackQuery.Data) ? update.CallbackQuery.Data : default;
            IsGameQuery = update.CallbackQuery.IsGameQuery;
        }
    }
}