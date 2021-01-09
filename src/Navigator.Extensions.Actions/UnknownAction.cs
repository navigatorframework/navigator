using Navigator.Abstraction;
using Navigator.Actions;
 using Navigator.Actions.Abstraction;

 namespace Navigator.Extensions.Actions
{
    public abstract class UnknownAction : Action
    {
        public override string Type => ActionType.Unknown;
    }
}