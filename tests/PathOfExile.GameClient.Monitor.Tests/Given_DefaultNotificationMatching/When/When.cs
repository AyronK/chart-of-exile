using System;
using System.Threading.Tasks;
using FluentAssertions;
using PathOfExile.GameClient.Monitor.Notifications.Abstraction;
using PathOfExile.GameClient.Monitor.Notifications.Metadata;

namespace PathOfExile.GameClient.Monitor.Tests.Given_DefaultNotificationMatching.When
{
    public class When : GivenDefaultNotificationMatching
    {
        private Task<(bool IsMatch, INotification Notification)> when;

        protected LogMetadata DefaultLogMetadata = new LogMetadata(new DateTime(2019, 06, 12, 21, 45, 30), LogLevel.Info);
        protected string DefaultLogMetadataText = "2019/06/12 21:45:30 3575671 ce8 [INFO Client 6616]";
        protected string NotificationText;

        protected void WhenIsMatch()
        {
            when = Task.Run(() => (NotificationMatch.IsMatch(NotificationText, out INotification notification), notification));
        }

        protected void Then_IsMatch(bool expected = true)
        {
            when.Result.IsMatch.Should().Be(expected);
        }

        protected void Then_Notification(INotification expected)
        {
            when.Result.Notification.Should().NotBeNull();
            when.Result.Notification.Should().Be(expected);
        }
    }
}