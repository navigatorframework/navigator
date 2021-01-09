using Navigator.Abstraction;
using Navigator.Actions;
 using Navigator.Actions.Abstraction;

 namespace Navigator.Extensions.Actions
{
    public abstract class ShippingQueryAction : Action
    {
        public override string Type => ActionType.ShippingQuery;
    }
}