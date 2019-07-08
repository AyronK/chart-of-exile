using PathOfExile.GameClient.Monitor.Notifications.Metadata;

namespace PathOfExile.GameClient.Monitor.Notifications.Abstraction
{
    public interface INotification
    {
        LogMetadata Metadata { get; }
    }
}