using Navigator.Actions;
using Navigator.Context;
using Navigator.Context.Extensions;
using Telegram.Bot.Types;

namespace Navigator.Providers.Telegram.Actions.Update
{
    /// <summary>
    /// Inline result based action.
    /// </summary>
    public abstract class ChosenInlineResultAction : BaseAction
    {
        /// <inheritdoc />
        public override string Type { get; protected set; } = typeof(ChosenInlineResultAction).FullName!;

        /// <inheritdoc />
        public override IAction Init(INavigatorContext navigatorContext)
        {
            var update = navigatorContext.GetOriginalUpdateOrDefault<global::Telegram.Bot.Types.Update>();

            if (update is not null)
            {
                ChosenInlineResult = update.ChosenInlineResult;
                ResultId = update.ChosenInlineResult.ResultId;
                Query = update.ChosenInlineResult.Query;
            }

            return this;
        }
        
        /// <summary>
        /// The original chosen inline result.
        /// </summary>
        public ChosenInlineResult ChosenInlineResult { get; protected set; } = null!;
        
        /// <summary>
        /// The chosen result id.
        /// </summary>
        public string ResultId { get; protected set; } = string.Empty;
        
        /// <summary>
        /// The original query.
        /// </summary>
        public string Query { get; protected set; } = string.Empty;
    }
}