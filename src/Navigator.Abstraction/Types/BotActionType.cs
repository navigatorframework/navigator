﻿namespace Navigator.Core.Abstractions.Types
{
    public enum BotActionType
    {
        Update,
        CallbackQuery,
        InlineQuery,
        InlineResultChosen,
        MessageEdited,
        Text,
        Command,
        Photo,
        Sticker,
        Video,
        Other
    }
}