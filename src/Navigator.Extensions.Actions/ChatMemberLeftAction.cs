﻿using Navigator.Abstraction;
using Navigator.Actions;

namespace Navigator.Extensions.Actions
{
    public abstract class ChatMemberLeftAction : Action
    {
        public override string Type => ActionType.ChatMemberLeft;
    }
}