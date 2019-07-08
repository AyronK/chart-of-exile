using System;
using PathOfExile.GameClient.Monitor.Notifications.Abstraction;

namespace PathOfExile.GameClient.Monitor.Monitoring
{
    public class NotificationEventArgs : EventArgs
    {
        public INotification Notification { get; }

        public NotificationEventArgs(INotification notification)
        {
            Notification = notification;
        }
    }
}