using System;

namespace PathOfExile.GameClient.Monitor.LogTracing
{
    public interface ILogMonitor
    {
        event EventHandler<EntryCreatedEventArgs> EntryCreated;
    }
}