using Navigator.Actions;
using Navigator.Context;
using Navigator.Providers.Telegram.Extensions;

namespace Navigator.Providers.Telegram.Actions.Messages
{
    /// <summary>
    /// Command based action.
    /// </summary>
    public abstract class CommandAction : MessageAction
    {
        /// <inheritdoc />
        public override string Type { get; protected set; } = typeof(CommandAction).FullName!;

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