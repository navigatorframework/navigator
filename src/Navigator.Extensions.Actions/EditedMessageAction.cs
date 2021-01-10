using Navigator.Abstractions;

namespace Navigator.Extensions.Actions
{
    public abstract class EditedMessageAction : MessageAction
    {
        public override string Type => ActionType.EditedMessage;
    }
}