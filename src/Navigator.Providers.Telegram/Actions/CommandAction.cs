using Navigator.Actions;
using Navigator.Actions.Model;
using Navigator.Context;
using Navigator.Providers.Telegram.Extensions;

namespace Navigator.Providers.Telegram.Actions
{
    /// <summary>
    /// Command based action.
    /// </summary>
    public abstract class CommandAction : MessageAction
    {
        /// <inheritdoc />
        public override string Type { get; protected set; } = ActionsHelper.Type.For<TelegramNavigatorProvider>(nameof(CommandAction));

        /// <inheritdoc />
        public override ushort Priority { get; protected set; } = ActionsHelper.Priority.Default;

        /// <inheritdoc />
        public override IAction Init(INavigatorContext navigatorContext)
        {
            Command = Message.ExtractCommand(navigatorContext.BotProfile.Username) ?? string.Empty;

            Arguments = Message.ExtractArguments(navigatorContext.BotProfile.Username);

            return this;    
        }
        
        /// <summary>
        /// Command.
        /// </summary>
        public string Command { get; protected set; } = null!;
        
        /// <summary>
        /// Any arguments passed with the command. If no arguments were passed, it will be null.
        /// </summary>
        public string? Arguments { get; protected set; }
        
    }
}