using System.Text.RegularExpressions;
using PathOfExile.GameClient.Monitor.Matching;
using PathOfExile.GameClient.Monitor.Notifications.Abstraction;
using PathOfExile.GameClient.Monitor.Notifications.Metadata;

namespace PathOfExile.GameClient.Monitor.Tests.Given_NotificationMatch
{
    public class GivenNotificationMatch
    {
        protected NotificationMatch NotificationMatch;
        
        protected void WithNotificationMatch(string regex = ".+")
        {
            NotificationMatch = new NotificationMatch
            (
                new Regex($"({regex})"),
                (collection, metadata) => new TestNotification(collection[1].Value, metadata)
            );
        }

        protected class TestNotification : INotification
        {
            public TestNotification(string text, LogMetadata metadata)
            {
                Text = text;
                Metadata = metadata;
            }

            public string Text { get; }

            public LogMetadata Metadata { get; }

            public override bool Equals(object obj)
            {
                return obj is TestNotification other && other.Metadata.Equals(Metadata) && other.Text == Text;
            }

            public override int GetHashCode()
            {
                unchecked
                {
                    return ((Metadata != null ? Metadata.GetHashCode() : 0) * 397) ^
                           (Text != null ? Text.GetHashCode() : 0);
                }
            }
        }
    }
}