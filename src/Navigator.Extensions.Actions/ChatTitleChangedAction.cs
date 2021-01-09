using Navigator.Abstraction;
using Navigator.Actions;
 using Navigator.Actions.Abstraction;

 namespace Navigator.Extensions.Actions
{
    public abstract class ChatTitleChangedAction : Action
    {
        public override string Type => ActionType.ChatTitleChanged;
    }
}