﻿using Navigator.Abstractions;
using Navigator.Actions;

namespace Navigator.Extensions.Actions
{
    public abstract class ChatPhotoChangedAction : Action
    {
        public override string Type => ActionType.ChatPhotoChanged;
    }
}