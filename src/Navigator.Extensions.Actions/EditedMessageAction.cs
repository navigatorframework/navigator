﻿using Navigator.Abstraction;

namespace Navigator.Extensions.Actions
{
    public abstract class EditedMessageAction : MessageAction
    {
        public override string Type => ActionType.EditedMessage;
    }
}