namespace Navigator.Actions.Attributes;

/// <summary>
/// Sets the priority of an action when multiple actions are available for the same trigger.
/// </summary>
[System.AttributeUsage(System.AttributeTargets.Class)]
public class ActionPriorityAttribute : System.Attribute
{
    public readonly ushort Priority;
    
    public ActionPriorityAttribute(ushort priority)
    {
        Priority = priority;
    }
}