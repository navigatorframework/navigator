namespace Navigator.Actions.Attributes;

/// <summary>
/// Sets the priority of an action when multiple actions are available for the same trigger.
/// </summary>
[System.AttributeUsage(System.AttributeTargets.Class)]
public class ActionPriorityAttribute : System.Attribute
{
    /// <summary>
    /// Priority.
    /// </summary>
    public readonly ushort Priority;
    
    /// <summary>
    /// Sets the action priority.
    /// </summary>
    /// <param name="priority"></param>
    public ActionPriorityAttribute(ushort priority)
    {
        Priority = priority;
    }
}