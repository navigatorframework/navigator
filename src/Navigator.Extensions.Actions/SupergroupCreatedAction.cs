using Navigator.Abstractions;
using Navigator.Actions;

namespace Navigator.Extensions.Actions
{
    public abstract class SupergroupCreatedAction : Action
    {
        public override string Type => ActionType.SupergroupCreated;
    }
}