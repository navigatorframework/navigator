namespace Navigator.Actions.Attributes;

[System.AttributeUsage(System.AttributeTargets.Class)]
public class ActionTypeAttribute : System.Attribute
{
    private string _action;
    public ActionTypeAttribute(string action)
    {
        _action = action;
    }
}