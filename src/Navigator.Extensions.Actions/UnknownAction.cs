using Navigator.Abstractions;

namespace Navigator.Extensions.Actions
{
    public abstract class UnknownAction : Action
    {
        public override string Type => ActionType.Unknown;
    }
}