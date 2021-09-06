using Navigator.Actions;
using Navigator.Context;
using Navigator.Context.Extensions;
using Telegram.Bot.Types;

namespace Navigator.Providers.Telegram.Actions.Updates
{
    /// <summary>
    /// Inline query based action.
    /// </summary>
    public abstract class InlineQueryAction : BaseAction
    {
        /// <inheritdoc />
        public override string Type { get; protected set; } = typeof(InlineQueryAction).FullName!;

        /// <summary>
        /// The original <see cref="Update.InlineQuery"/>
        /// </summary>
        public InlineQuery InlineQuery { get; protected set; } = null!;
        
        /// <summary>
        /// The query from the user.
        /// </summary>
        public string Query { get; protected set; } = string.Empty;
        
        /// <summary>
        /// The offset.
        /// </summary>
        public string Offset { get; protected set; } = string.Empty;

        /// <inheritdoc />
        protected InlineQueryAction(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
        {
            var update = NavigatorContextAccessor.NavigatorContext.GetOriginalEvent<Update>();
            
            InlineQuery = update.InlineQuery;
            Query = update.InlineQuery.Query;
            Offset = update.InlineQuery.Offset;
        }
    }
}