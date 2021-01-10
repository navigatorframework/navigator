using Navigator.Abstractions;

namespace Navigator.Extensions.Actions
{
    public abstract class EditedChannelPostAction : ChannelPostAction
    {
        public override string Type => ActionType.EditedChannelPost;
    }
}