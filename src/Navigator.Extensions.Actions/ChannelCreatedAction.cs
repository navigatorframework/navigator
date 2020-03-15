﻿using Navigator.Abstraction;
using Navigator.Actions;

namespace Navigator.Extensions.Actions
{
    public abstract class ChannelCreatedAction : Action
    {
        public override string Type => ActionType.ChannelCreated;
    }
}