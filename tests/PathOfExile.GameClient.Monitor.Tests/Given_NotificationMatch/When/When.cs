using System;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using PathOfExile.GameClient.Monitor.Notifications.Abstraction;
using PathOfExile.GameClient.Monitor.Notifications.Metadata;

namespace PathOfExile.GameClient.Monitor.Tests.Given_NotificationMatch.When
{
    public class When : GivenNotificationMatch
    {
        private string notificationText;
        private Task<(bool IsMatch, INotification Notification)> when;

        [TestCase("INFO", LogLevel.Info)]
        [TestCase("CRIT", LogLevel.Crit)]
        [TestCase("DEBUG", LogLevel.Debug)]
        [TestCase("WARN", LogLevel.Warn)]
        public void And_IsMatch_And_CorrectLogMetadata(string logLevelText, LogLevel expectedLogLevel)
        {
            WithNotificationMatch();

            notificationText = $"2019/06/12 21:45:30 3575671 ce8 [{logLevelText} Client 6616] Test message";

            WhenIsMatch();

            Then_Metadata(new LogMetadata(new DateTime(2019, 06, 12, 21, 45, 30), expectedLogLevel));
        }

        [TestCase("Text", true)]
        [TestCase("(^&*^&*^&*(^", false)]
        public void And_IsMatch_And_CorrectResult(string messageText, bool isMatch)
        {
            WithNotificationMatch(@"\w+");

            notificationText = $"2019/06/12 21:45:30 3575671 ce8 [INFO Client 6616] {messageText}";

            WhenIsMatch();

            Then_IsMatch(isMatch);
        }

        [Test]
        public void And_IsMatch_And_CorrectNotification()
        {
            WithNotificationMatch();

            var messageText = "Test message";
            notificationText = $"2019/06/12 21:45:30 3575671 ce8 [INFO Client 6616] {messageText}";

            WhenIsMatch();

            Then_IsMatch();
            Then_Notification(new TestNotification(messageText, new LogMetadata(new DateTime(2019, 06, 12, 21, 45, 30), LogLevel.Info)));
        }

        private void WhenIsMatch()
        {
            when = Task.Run(() => (NotificationMatch.IsMatch(notificationText, out INotification notification), notification));
        }

        private void Then_IsMatch(bool expected = true)
        {
            when.Result.IsMatch.Should().Be(expected);
        }

        private void Then_Metadata(LogMetadata expected)
        {
            when.Result.Notification.Should().NotBeNull();
            when.Result.Notification.Metadata.Should().Be(expected);
        }

        private void Then_Notification(TestNotification expected)
        {
            when.Result.Notification.Should().NotBeNull();
            when.Result.Notification.Should().Be(expected);
        }
    }
}