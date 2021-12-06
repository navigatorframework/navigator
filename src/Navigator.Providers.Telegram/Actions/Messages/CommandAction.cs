using Navigator.Actions;
using Navigator.Actions.Attributes;
using Navigator.Context;
using Navigator.Providers.Telegram.Extensions;

namespace Navigator.Providers.Telegram.Actions.Messages
{
    /// <summary>
    /// Command based action.
    /// </summary>
    [ActionType(nameof(CommandAction))]
    public abstract class CommandAction : MessageAction
    {
        /// <summary>
        /// Command.
        /// </summary>
        public readonly string Command;

        /// <summary>
        /// Any arguments passed with the command. If no arguments were passed, it will be null.
        /// </summary>
        public readonly string? Arguments;

        /// <inheritdoc />
        protected CommandAction(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
        {
            var botProfile = NavigatorContextAccessor.NavigatorContext.BotProfile;

            Command = Message.ExtractCommand(botProfile.Username) ?? string.Empty;
            Arguments = Message.ExtractArguments(botProfile.Username);
        }
    }
}