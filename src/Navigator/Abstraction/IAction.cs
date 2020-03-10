using System;
using MediatR;

namespace Navigator.Abstraction
{
    public interface IAction : IRequest
    {
        DateTime Timestamp { get; }
    }
}