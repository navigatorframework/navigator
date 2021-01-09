using System;
using MediatR;

namespace Navigator.Notification.Abstraction
{
    public interface INavigatorNotification : INotification
    {
        DateTime Timestamp { get; }
        int UpdateId { get; }
    }
}