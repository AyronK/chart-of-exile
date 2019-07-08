using System;

namespace PathOfExile.GameClient.Monitor.Notifications.Metadata
{
    public class LogMetadata
    {
        public DateTime Timestamp { get; }
        public LogLevel? LogLevel { get; }

        public LogMetadata(DateTime timestamp)
        {
            Timestamp = timestamp;
        }

        public LogMetadata(DateTime timestamp, LogLevel logLevel)
        {
            Timestamp = timestamp;
            LogLevel = logLevel;
        }

        public override bool Equals(object obj) => obj is LogMetadata other && other.Timestamp == Timestamp && other.LogLevel == LogLevel;
        
        public override int GetHashCode()
        {
            unchecked
            {
                return (Timestamp.GetHashCode() * 397) ^ LogLevel.GetHashCode();
            }
        }
    }
}