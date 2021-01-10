using Navigator.Abstractions;

namespace Navigator.Extensions.Actions
{
    public abstract class PreCheckoutQueryAction : Action
    {
        public override string Type => ActionType.PreCheckoutQuery;
    }
}