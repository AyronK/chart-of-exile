using PathOfExile.GameClient.Monitor.Notifications.Abstraction;

namespace PathOfExile.GameClient.Monitor.Matching
{
    public interface INotificationMatch
    {
        bool IsMatch(string text, out INotification notification);
    }
}