using Navigator.Abstractions;

namespace Navigator.Extensions.Actions
{
    public abstract class MigratedToSupergroupAction : Action
    {
        public override string Type => ActionType.MigratedToSupergroup;
    }
}