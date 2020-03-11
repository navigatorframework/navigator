using System;
using MediatR;

namespace Navigator.Abstraction
{
    public interface INavigatorNotification : INotification
    {
        DateTime Timestamp { get; }
        int UpdateId { get; }
    }
}