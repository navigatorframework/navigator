using Navigator.Abstractions;
using Navigator.Actions;

namespace Navigator.Extensions.Actions
{
    public abstract class ChatPhotoDeletedAction : Action
    {
        public override string Type => ActionType.ChatPhotoDeleted;
    }
}