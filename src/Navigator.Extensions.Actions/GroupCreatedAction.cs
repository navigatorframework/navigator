﻿using Navigator.Abstraction;
using Navigator.Actions;

namespace Navigator.Extensions.Actions
{
    public abstract class GroupCreatedAction : Action
    {
        public override string Type => ActionType.GroupCreated;
    }
}