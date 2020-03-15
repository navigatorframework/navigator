﻿using Navigator.Abstraction;
using Navigator.Actions;

namespace Navigator.Extensions.Actions
{
    public abstract class InlineResultChosenAction : Action
    {
        public override string Type => ActionType.InlineResultChosen;
        public string ChosenResultId { get; protected set; } = string.Empty;
        public string Query { get; protected set; } = string.Empty;
        
        public override IAction Init(INavigatorContext ctx)
        {
            ChosenResultId = ctx.Update.ChosenInlineResult.ResultId;
            Query = ctx.Update.ChosenInlineResult.Query;

            return this;
        }
    }
}