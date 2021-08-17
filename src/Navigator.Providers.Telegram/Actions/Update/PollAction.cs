using Navigator.Actions;
using Navigator.Context;
using Navigator.Context.Extensions;
using Telegram.Bot.Types;

namespace Navigator.Providers.Telegram.Actions.Update
{
    public abstract class PollAction : BaseAction
    {
        /// <inheritdoc />
        public override string Type { get; protected set; } = typeof(PollAction).FullName!;

        /// <inheritdoc />
        public override IAction Init(INavigatorContext navigatorContext)
        {
            var update = navigatorContext.GetOriginalUpdateOrDefault<global::Telegram.Bot.Types.Update>();

            if (update is not null)
            {
                Poll = update.Poll;
            }

            return this;
        }

        /// <summary>
        /// The original Poll.
        /// </summary>
        public Poll Poll { get; protected set; } = null!;
    }
}