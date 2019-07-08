using PathOfExile.GameClient.Monitor.Notifications.Metadata;

namespace PathOfExile.GameClient.Monitor.Notifications.Abstraction
{
    public abstract class BaseNotification : INotification
    {
        protected BaseNotification(LogMetadata metadata)
        {
            Metadata = metadata;
        }

        public LogMetadata Metadata { get; }

        public override bool Equals(object obj)
        {
            return obj is BaseNotification other && other.Metadata.Equals(Metadata);
        }

        public override int GetHashCode()
        {
            return Metadata != null ? Metadata.GetHashCode() : 0;
        }
    }
}