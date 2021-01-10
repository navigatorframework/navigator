using Navigator.Abstractions;

namespace Navigator.Extensions.Actions
{
    public abstract class PollAction : Action
    {
        public override string Type => ActionType.Poll;
    }
}