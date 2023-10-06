namespace Navigator.Actions.Attributes;

[System.AttributeUsage(System.AttributeTargets.Class)]
public class ActionTypeAttribute : System.Attribute
{
    public string ActionType;
    
    public ActionTypeAttribute(string action)
    {
        ActionType = action;
    }
}