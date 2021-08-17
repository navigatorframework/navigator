using Navigator.Actions;
using Navigator.Context;
using Navigator.Context.Extensions;
using Telegram.Bot.Types;

namespace Navigator.Providers.Telegram.Actions.Update
{
    /// <summary>
    /// Inline query based action.
    /// </summary>
    public abstract class InlineQueryAction : BaseAction
    {
        /// <inheritdoc />
        public override string Type { get; protected set; } = typeof(InlineQueryAction).FullName!;

        /// <inheritdoc />
        public override IAction Init(INavigatorContext navigatorContext)
        {
            var update = navigatorContext.GetOriginalUpdateOrDefault<global::Telegram.Bot.Types.Update>();

            if (update is not null)
            {
                InlineQuery = update.InlineQuery;
                Query = update.InlineQuery.Query;
                Offset = update.InlineQuery.Offset;
            }

            return this;
        }
        
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
    }
}