using System;
using System.Globalization;
using System.Text.RegularExpressions;
using PathOfExile.GameClient.Monitor.Notifications.Abstraction;
using PathOfExile.GameClient.Monitor.Notifications.Metadata;

namespace PathOfExile.GameClient.Monitor.Matching
{
    public sealed class NotificationMatch : INotificationMatch
    {
        public override bool Equals(object obj) => obj is NotificationMatch other && other.Regex.Equals(Regex) && other.OnMap.Equals(OnMap);

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Regex != null ? Regex.GetHashCode() : 0) * 397) ^ (OnMap != null ? OnMap.GetHashCode() : 0);
            }
        }

        private readonly Regex timestampRegex = new Regex(@"([0-9]{4}\/[0-9]{2}\/[0-9]{2} [0-9]{2}:[0-9]{2}:[0-9]{2})(.*)");
        private readonly Regex logLevelRegex = new Regex(@"\[(.*) Client [0-9]*] (.*)");

        public Regex Regex { get; }
        public Func<GroupCollection, LogMetadata, INotification> OnMap { get; }

        public NotificationMatch(Regex regex, Func<GroupCollection, LogMetadata, INotification> onMap)
        {
            Regex = regex;
            OnMap = onMap;
        }   

        public bool IsMatch(string text, out INotification notification)
        {
            LogMetadata logMetadata = ParseMetadata(text, out string textSeparatedFromMetadata);
            if (logMetadata is null)
            {
                notification = null;
                return false;
            }

            Match match = Regex.Match(textSeparatedFromMetadata);
            notification = match.Success ? OnMap(match.Groups, logMetadata) : null;
            return match.Success;
        }

        private LogMetadata ParseMetadata(string logMessage, out string unparsedText)
        {
            unparsedText = logMessage;
            Match match = timestampRegex.Match(unparsedText);
            DateTime timeStamp = DateTime.ParseExact(match.Groups[1].Value, "yyyy/MM/dd HH:mm:ss", CultureInfo.CurrentCulture);

            if (!match.Success)
            {
                return null;
            }

            unparsedText = match.Groups[2].Value;
            match = logLevelRegex.Match(unparsedText);

            if (!match.Success || !Enum.TryParse(match.Groups[1].Value, ignoreCase: true, out LogLevel logLevel))
            {
                return new LogMetadata(timeStamp);
            }

            unparsedText = match.Groups[2].Value;
            return new LogMetadata(timeStamp, logLevel);
        }
    }
}