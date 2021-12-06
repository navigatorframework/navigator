namespace Navigator.Actions.Attributes;

[System.AttributeUsage(System.AttributeTargets.Class)]
public class ActionPriorityAttribute : System.Attribute
{
    private ushort _priority;
    public ActionPriorityAttribute(ushort priority)
    {
        _priority = priority;
    }
}