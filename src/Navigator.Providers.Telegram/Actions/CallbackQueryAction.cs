using Navigator.Actions;
using Navigator.Context;
using Navigator.Context.Extensions;
using Telegram.Bot.Types;

namespace Navigator.Providers.Telegram.Actions
{
    /// <summary>
    /// A callback query based action.
    /// </summary>
    public abstract class CallbackQueryAction : BaseAction
    {
        /// <inheritdoc />
        public override string Type { get; protected set; } = ActionsHelper.Type.For<TelegramNavigatorProvider>(nameof(CallbackQueryAction));

        /// <inheritdoc />
        public override IAction Init(INavigatorContext navigatorContext)
        {
            var update = navigatorContext.GetOriginalUpdateOrDefault<Update>();
            
            if (update is not null)
            {
                CallbackQuery = update.CallbackQuery;
                OriginalMessage = update.CallbackQuery.Message;
                Data = string.IsNullOrWhiteSpace(update.CallbackQuery.Data) ? update.CallbackQuery.Data : default;
                IsGameQuery = update.CallbackQuery.IsGameQuery;
            }

            return this;  
        }

        /// <summary>
        /// The original <see cref="Update.CallbackQuery"/>
        /// </summary>
        public CallbackQuery CallbackQuery { get; protected set; } = null!;

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

    }
}