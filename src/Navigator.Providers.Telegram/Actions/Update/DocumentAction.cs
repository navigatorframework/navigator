using Navigator.Actions;
using Navigator.Context;
using Navigator.Context.Extensions;
using Telegram.Bot.Types;

namespace Navigator.Providers.Telegram.Actions.Update
{
    public class DocumentAction : BaseAction
    {
        public override string Type { get; protected set; } = typeof(DocumentAction).FullName!;
        
        public override IAction Init(INavigatorContext navigatorContext)
        {
            throw new System.NotImplementedException();
        }

        public override bool CanHandle(INavigatorContext ctx)
        {
            throw new System.NotImplementedException();
        }
    }
}