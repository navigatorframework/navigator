using Navigator.Abstractions;
using Navigator.Actions;

namespace Navigator.Extensions.Actions
{
    public abstract class PreCheckoutQueryAction : Action
    {
        public override string Type => ActionType.PreCheckoutQuery;
    }
}