using System;
using MediatR;

namespace Navigator.Abstractions
{
    public interface INavigatorNotification : INotification
    {
        DateTime Timestamp { get; }
        int UpdateId { get; }
    }
}