using Navigator.Abstractions;
using Navigator.Actions;

namespace Navigator.Extensions.Actions
{
    public abstract class CallbackQueryAction : Action
    {
        public override string Type => ActionType.CallbackQuery;

        public string Data { get; set; } = string.Empty;

        public bool IsGameQuery { get; set; }

        public override IAction Init(INavigatorContext ctx)
        {
            Data = ctx.Update.CallbackQuery.Data;
            IsGameQuery = ctx.Update.CallbackQuery.IsGameQuery;
            
            return this;
        }
    }
}