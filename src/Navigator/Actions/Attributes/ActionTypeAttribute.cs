namespace Navigator.Actions.Attributes;

/// <summary>
/// Attribute for setting the action type.
/// </summary>
[System.AttributeUsage(System.AttributeTargets.Class)]
public class ActionTypeAttribute : System.Attribute
{
    /// <summary>
    /// Action type.
    /// </summary>
    public readonly string ActionType;
    
    /// <summary>
    /// Sets the action type.
    /// </summary>
    /// <param name="action"></param>
    public ActionTypeAttribute(string action)
    {
        ActionType = action;
    }
}