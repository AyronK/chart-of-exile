using System;
using System.Collections.Generic;
using PathOfExile.GameClient.Monitor.LogTracing;
using PathOfExile.GameClient.Monitor.Matching;
using PathOfExile.GameClient.Monitor.Notifications.Abstraction;

namespace PathOfExile.GameClient.Monitor.Monitoring
{
    internal sealed class NotificationMonitor : IDisposable, INotificationMonitor
    {
        private readonly ILogMonitor logMonitor;
        private readonly bool isOnlyFirstMatchHandled;
        private readonly List<INotificationMatch> notificationMatches = new List<INotificationMatch>();

        public event EventHandler<NotificationEventArgs> NotificationReceived;

        public NotificationMonitor(ILogMonitor logMonitor, bool isOnlyFirstMatchHandled = false)
        {
            this.logMonitor = logMonitor;
            this.isOnlyFirstMatchHandled = isOnlyFirstMatchHandled;
            logMonitor.EntryCreated += LogMonitorOnEntryCreated;
        }

        public void RegisterMatching(INotificationMatch match)
        {
            if (notificationMatches.Contains(match))
            {
                throw new ArgumentException($"Matching of type {match.GetType()} is already registered.", nameof(match));
            }

            notificationMatches.Add(match);
        }

        private void LogMonitorOnEntryCreated(object sender, EntryCreatedEventArgs e)
        {
            foreach (INotificationMatch match in notificationMatches)
            {
                if (!match.IsMatch(e.LogEntryValue, out INotification notification))
                {
                    continue;
                }

                OnNotificationReceived(notification);

                if (isOnlyFirstMatchHandled)
                {
                    return;
                }
            }
        }

        private void OnNotificationReceived(INotification notification)
        {
            NotificationReceived?.Invoke(this, new NotificationEventArgs(notification));
        }

        public void Dispose()
        {
            logMonitor.EntryCreated -= LogMonitorOnEntryCreated;
        }
    }
}
