﻿using Navigator.Abstractions;
using Navigator.Actions;

namespace Navigator.Extensions.Actions
{
    public abstract class MigratedFromGroupAction : Action
    {
        public override string Type => ActionType.MigratedFromGroup;
    }
}