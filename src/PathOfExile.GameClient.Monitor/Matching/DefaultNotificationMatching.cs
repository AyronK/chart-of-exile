using System;
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
            regex: new Regex(@": ([\p{L}\p{Nd}]+) \(\w+\) is now level (\d*)"),
            onMap: (groups, metadata) =>
                new LevelUpNotification(groups[1].Value, short.Parse(groups[2].Value), metadata)
        );

        internal static INotificationMatch ChatMessageNotificationMatch { get; } = new NotificationMatch
        (
            regex: new Regex(@"([#$%&]|@From |@To )?(?:<([\p{L}\p{Nd}]+)> )?([\p{L}\p{Nd}]+): (.*)"),
            onMap: (groups, metadata) => new ChatMessageNotification(ParseChannel(groups[1].Value.Trim()), groups[3].Value, groups[2].Value, groups[4].Value, metadata)
        );

        private static ChatChannel ParseChannel(string text)
        {
            switch (text)
            {
                case "":
                    return ChatChannel.Local;
                case "#":
                    return ChatChannel.Global;
                case "%":
                    return ChatChannel.Party;
                case "$":
                    return ChatChannel.Trade;
                case "&":
                    return ChatChannel.Guild;
                case "@From":
                    return ChatChannel.WhisperFrom;
                case "@To":
                    return ChatChannel.WhisperTo;
                default:
                    throw new NotSupportedException($"{text} is not supported chat message channel.");
            }
        }
    }
}
