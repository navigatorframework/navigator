using System.Threading.Tasks;
using Navigator.Actions;
using Navigator.Context;
using Navigator.Context.Extensions;
using Telegram.Bot.Types;

namespace Navigator.Providers.Telegram.Actions
{
    public abstract class MessageAction : BaseAction
    {
        public override string Type { get; protected set; } = ActionHelper.Type.For<TelegramProvider>(nameof(MessageAction));
        public override ushort Priority { get; protected set; } = ActionHelper.Priority.Default;

        public override IAction Init(INavigatorContext navigatorContext)
        {
            var update = navigatorContext.GetOriginalUpdateOrDefault<Update>();

            if (update is not null)
            {
                Message = update.Message;
            }

            return this;    
        }
        
        public Message Message { get; protected set; }
    }
}