using Navigator.Actions;
using Navigator.Actions.Model;
using Navigator.Context;

namespace Navigator.Providers.Telegram.Actions
{
    public abstract class CommandAction : MessageAction
    {
        protected CommandAction()
        {
            Command = string.Empty;
            Arguments = string.Empty;
        }

        public override string Type { get; protected set; } = ActionsHelper.Type.For<TelegramNavigatorProvider>(nameof(CommandAction));
        public override ushort Priority { get; protected set; } = ActionsHelper.Priority.Default;
        
        public override IAction Init(INavigatorContext navigatorContext)
        {
            Command = Message.ExtractCommand(navigatorContext.BotProfile.Username) ?? string.Empty;

            Arguments = Message.ExtractArguments(navigatorContext.BotProfile.Username) ?? string.Empty;

            return this;    
        }
        
        public string Command { get; protected set; }
        public string Arguments { get; protected set; }
        
    }
}