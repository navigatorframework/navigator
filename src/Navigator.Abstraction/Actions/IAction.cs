﻿using MediatR;
using Navigator.Core.Abstractions.Types;

namespace Navigator.Core.Abstractions.Actions
{
    public interface IAction : IRequest
    {
        BotActionType Type { get; }
        IAction Fill(INavigatorContext navigatorContext);
        bool CanHandle(INavigatorContext navigatorContext);
    }
}