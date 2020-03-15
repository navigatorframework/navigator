﻿using System;
using Navigator.Abstraction;
using Action = Navigator.Actions.Action;

namespace Navigator.Extensions.Actions
{
    public abstract class ChannelPostAction : Action
    {
        public override string Type => ActionType.ChannelPost;
        public DateTime PostTimestamp { get; protected set; }
        public int PostId { get; protected set; }

        public override IAction Init(INavigatorContext ctx)
        {
            PostTimestamp = ctx.Update.ChannelPost.Date;
            PostId = ctx.Update.ChannelPost.MessageId;
            
            return this;
        }

    }
}