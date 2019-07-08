using System;

namespace PathOfExile.GameClient.Monitor.Monitoring
{
    public interface INotificationMonitor
    {
        event EventHandler<NotificationEventArgs> NotificationReceived;
    }
}