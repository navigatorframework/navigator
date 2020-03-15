﻿using Navigator.Abstraction;
using Navigator.Actions;

namespace Navigator.Extensions.Actions
{
    public abstract class MessagePinnedAction : Action
    {
        public override string Type => ActionType.MessagePinned;
    }
}