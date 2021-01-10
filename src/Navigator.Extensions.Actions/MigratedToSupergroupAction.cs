using Navigator.Abstractions;
using Navigator.Actions;

namespace Navigator.Extensions.Actions
{
    public abstract class MigratedToSupergroupAction : Action
    {
        public override string Type => ActionType.MigratedToSupergroup;
    }
}