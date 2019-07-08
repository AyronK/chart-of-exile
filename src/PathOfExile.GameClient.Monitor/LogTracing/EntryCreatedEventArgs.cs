using System;

namespace PathOfExile.GameClient.Monitor.LogTracing
{
    public class EntryCreatedEventArgs : EventArgs
    {
        public string LogEntryValue { get; set; }

        public EntryCreatedEventArgs(string logEntryValue)
        {
            LogEntryValue = logEntryValue;
        }
    }
}