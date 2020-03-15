﻿using Navigator.Abstraction;
using Navigator.Actions;

namespace Navigator.Extensions.Actions
{
    public abstract class ChatTitleChangedAction : Action
    {
        public override string Type => ActionType.ChatTitleChanged;
    }
}