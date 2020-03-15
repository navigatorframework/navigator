﻿using System;
using Navigator.Abstraction;
using Action = Navigator.Actions.Action;

namespace Navigator.Extensions.Actions
{
    public abstract class InlineQueryAction : Action
    {
        public override string Type => ActionType.InlineQuery;
        public string InlineQueryId { get; protected set; }
        public string Query { get; protected set; }
        public string Offset { get; protected set; }

        public override IAction Init(INavigatorContext ctx)
        {
            InlineQueryId = ctx.Update.InlineQuery.Id;
            Query = ctx.Update.InlineQuery.Query;
            Offset = ctx.Update.InlineQuery.Offset;

            return this;
        }
    }
}