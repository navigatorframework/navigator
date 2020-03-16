using Navigator.Abstraction;
using Navigator.Actions;

namespace Navigator.Extensions.Actions
{
    public class CallbackQueryAction : Action
    {
        public override string Type => ActionType.CallbackQuery;
        public override IAction Init(INavigatorContext ctx)
        {
            throw new System.NotImplementedException();
        }

        public override bool CanHandle(INavigatorContext ctx)
        {
            throw new System.NotImplementedException();
        }
    }
}