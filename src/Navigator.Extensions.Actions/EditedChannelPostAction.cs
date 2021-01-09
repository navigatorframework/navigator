using Navigator.Abstraction;
 using Navigator.Actions.Abstraction;

 namespace Navigator.Extensions.Actions
{
    public abstract class EditedChannelPostAction : ChannelPostAction
    {
        public override string Type => ActionType.EditedChannelPost;
    }
}