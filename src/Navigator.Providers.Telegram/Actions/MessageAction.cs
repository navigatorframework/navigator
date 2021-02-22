using System.Threading.Tasks;
using Navigator.Actions;
using Navigator.Context;

namespace Navigator.Providers.Telegram.Actions
{
    public class MessageAction : BaseAction
    {
        public override string Type { get; protected set; } = Action.Type.For<TelegramProvider>("message");
        public override ushort Priority { get; protected set; } = Action.Priority.Default;
        
        public override Task Init(INavigatorContext navigatorContext)
        {
            throw new System.NotImplementedException();
        }
    }
}