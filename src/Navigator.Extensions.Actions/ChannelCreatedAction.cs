using Navigator.Actions;
using Navigator.Actions.Abstraction;

namespace Navigator.Extensions.Actions
{
    public abstract class ChannelCreatedAction : Action
    {
        public override string Type => ActionType.ChannelCreated;
    }
}