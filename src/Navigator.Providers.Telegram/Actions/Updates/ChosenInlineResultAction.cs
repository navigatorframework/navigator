using Navigator.Actions;
using Navigator.Context;
using Navigator.Context.Extensions;
using Telegram.Bot.Types;

namespace Navigator.Providers.Telegram.Actions.Updates
{
    /// <summary>
    /// Inline result based action.
    /// </summary>
    public abstract class ChosenInlineResultAction : BaseAction
    {
        /// <summary>
        /// The original chosen inline result.
        /// </summary>
        public ChosenInlineResult ChosenInlineResult { get; protected set; }
        
        /// <summary>
        /// The chosen result id.
        /// </summary>
        public string ResultId { get; protected set; }
        
        /// <summary>
        /// The original query.
        /// </summary>
        public string Query { get; protected set; }

        /// <inheritdoc />
        protected ChosenInlineResultAction(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
        {
            var update = NavigatorContextAccessor.NavigatorContext.GetOriginalEvent<Update>();

            ChosenInlineResult = update.ChosenInlineResult;
            ResultId = update.ChosenInlineResult.ResultId;
            Query = update.ChosenInlineResult.Query;
        }
    }
}