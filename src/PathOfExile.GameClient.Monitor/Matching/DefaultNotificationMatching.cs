using System;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using PathOfExile.GameClient.Monitor.Notifications;
using PathOfExile.GameClient.Monitor.Notifications.Constants;

namespace PathOfExile.GameClient.Monitor.Matching
{
    internal static class DefaultNotificationMatching
    {
        internal static INotificationMatch AreaEnterNotificationMatch { get; } = new NotificationMatch
        (
            regex: new Regex(@": You have entered (.*)\."),
            onMap: (groups, metadata) => new AreaChangedNotification(groups[1].Value.Trim(), metadata)
        );

        internal static INotificationMatch TextNotificationMatch { get; } = new NotificationMatch
        (
            regex: new Regex(".*"),
            onMap: (groups, metadata) => new TextNotification(groups[0].Value.Trim(), metadata)
        );

        internal static INotificationMatch AfkNotificationMatch { get; } = new NotificationMatch
        (
            regex: new Regex(@": AFK mode is now ((ON)|(OFF))\..*"),
            onMap: (groups, metadata) => new AfkNotification(groups[1].Value.Trim() == "ON", metadata)
        );

        internal static INotificationMatch LevelUpNotificationMatch { get; } = new NotificationMatch
        (
            regex: new Regex(@": ([\p{L}\p{Nd}]+) \(\w+\) is now level (\d*)"),
            onMap: (groups, metadata) =>
                new LevelUpNotification(groups[1].Value.Trim(), short.Parse(groups[2].Value.Trim()), metadata)
        );

        internal static INotificationMatch PlayerInAreaNotificationMatch { get; } = new NotificationMatch
        (
            regex: new Regex(@": ([\p{L}\p{Nd}]+) has (joined|left) the area."),
            onMap: (groups, metadata) => new PlayerInAreaNotification(groups[1].Value.Trim(), groups[2].Value.Trim() == "joined" ? PlayerAreaAction.Join : PlayerAreaAction.Leave, metadata)
        );

        internal static INotificationMatch ChatMessageNotificationMatch { get; } = new NotificationMatch
        (
            regex: new Regex(@"([#$%&]|@From |@To )?(?:<([\p{L}\p{Nd}]+)> )?([\p{L}\p{Nd}]+): (.*)"),
            onMap: (groups, metadata) => new ChatMessageNotification(ParseChannel(groups[1].Value.Trim()), groups[3].Value.Trim(), groups[2].Value.Trim(), groups[4].Value.Trim(), metadata)
        );

        internal static INotificationMatch TradeNotificationMatch { get; } = new NotificationMatch
        (
            regex: new Regex(@"@From (?:<[\p{L}\p{Nd}]+> )?([\p{L}\p{Nd}]+):.*buy your ([a-zA-Z\s'\d]+)\s*(?:\((.*)\))? listed for (\d+.?\d*) (.+) in (.+)\(stash tab ""(.+)""; position: left (\d+), top (\d+)\)\s*(.*)"),
            onMap: (groups,
                metadata) => new TradeNotification(
                groups[1].Value.Trim(),
                groups[2].Value.Trim(),
                groups[3].Value.Trim(),
                double.Parse(groups[4].Value.Trim(), NumberStyles.Any, CultureInfo.InvariantCulture),
                groups[5].Value.Trim(),
                groups[6].Value.Trim(),
                groups[7].Value.Trim(),
                new StashTabPosition
                {
                    Left = short.Parse(groups[8].Value.Trim()),
                    Top = short.Parse(groups[9].Value.Trim())
                },
                groups[10].Value.Trim(),
                metadata)
        );
        
        internal static INotificationMatch BulkTradeNotificationMatch { get; } = new NotificationMatch
        (
            regex: new Regex(@"@From (?:<[\p{L}\p{Nd}]+> )?([\p{L}\p{Nd}]+):.*buy your (\d*)\s*([a-zA-Z\s'\d]+)\s*(?:\((.*)\))? for my (\d+.?\d*) (.+) in (.+)\.\s*(.*)"),
            onMap: (groups,
                metadata) => new TradeNotification(
                groups[1].Value.Trim(),
                short.Parse(groups[2].Value.Trim()),
                groups[3].Value.Trim(),
                groups[4].Value.Trim(),
                double.Parse(groups[5].Value.Trim()),
                groups[6].Value.Trim(),
                groups[7].Value.Trim(),
                groups[8].Value.Trim(),
                metadata)
        );

        internal static INotificationMatch MapExchangeNotificationMatch { get; } = new NotificationMatch
        (
            regex: new Regex(@"@From (?:<[\p{L}\p{Nd}]+> )?([\p{L}\p{Nd}]+):.*exchange my (T\d+): \((.*)\) for your (T\d+): \((.*)\) in (.*)\.\s*(.*)"),
            onMap: (groups,
                metadata) => new MapExchangeNotification(
                groups[1].Value.Trim(),
                groups[2].Value.Trim(),
                groups[3].Value.Trim().Split(new []{','}, StringSplitOptions.RemoveEmptyEntries).Select(s=>s.Trim()).ToArray(),
                groups[4].Value.Trim(),
                groups[5].Value.Trim().Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(s => s.Trim()).ToArray(),
                groups[6].Value.Trim(),
                groups[7].Value.Trim(),
                metadata)
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
