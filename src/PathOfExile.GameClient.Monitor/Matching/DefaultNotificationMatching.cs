using System.Text.RegularExpressions;
using PathOfExile.GameClient.Monitor.Notifications;

namespace PathOfExile.GameClient.Monitor.Matching
{
    internal static class DefaultNotificationMatching
    {
        internal static INotificationMatch AreaEnterNotificationMatch { get; } = new NotificationMatch
        (
            regex: new Regex(@": You have entered (.*)\."),
            onMap: (groups, metadata) => new AreaChangedNotification(groups[1].Value, metadata)
        );

        internal static INotificationMatch TextNotificationMatch { get; } = new NotificationMatch
        (
            regex: new Regex(".*"),
            onMap: (groups, metadata) => new TextNotification(groups[0].Value, metadata)
        );

        internal static INotificationMatch AfkNotificationMatch { get; } = new NotificationMatch
        (
            regex: new Regex(@": AFK mode is now ((ON)|(OFF))\..*"),
            onMap: (groups, metadata) => new AfkNotification(groups[1].Value == "ON", metadata)
        );

        internal static INotificationMatch LevelUpNotificationMatch { get; } = new NotificationMatch
        (
            regex: new Regex(@": (.*) \(.*\) is now level (\d*)"),
            onMap: (groups, metadata) => new LevelUpNotification(groups[1].Value, short.Parse(groups[2].Value), metadata)
        );
    }
}
