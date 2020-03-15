﻿using Navigator.Abstraction;
using Navigator.Actions;

namespace Navigator.Extensions.Actions
{
    public abstract class ChatMembersAddedAction : Action
    {
        public override string Type => ActionType.ChatMembersAdded;
    }
}