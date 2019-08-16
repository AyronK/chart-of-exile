using NUnit.Framework;
using PathOfExile.GameClient.Monitor.Notifications;

namespace PathOfExile.GameClient.Monitor.Tests.Given_DefaultNotificationMatching.When
{
    public class WhenAfkNotificationMatch : When
    {
        [TestCase(": AFK mode is now ON. Autoreply \"This player is AFK.\"", true)]
        [TestCase(": AFK mode is now ON.", true)]
        [TestCase(": AFK mode is now OFF.", false)]
        public void And_IsMatch(string text, bool isAfk)
        {
            With_AfkNotificationMatch();

            NotificationText = $"{DefaultLogMetadataText} {text}.";

            WhenIsMatch();

            Then_IsMatch();
            Then_Notification(new AfkNotification(isAfk, DefaultLogMetadata));
        }

        [TestCase("AFK mode is now OFF.", Description = "Invalid format - no ':'")]
        [TestCase(": Trade accepted.")]
        [TestCase("Connect time to instance server was 32ms")]
        public void And_IsNotMatch(string messageText)
        {
            With_AfkNotificationMatch();

            NotificationText = $"{DefaultLogMetadataText} {messageText}";

            WhenIsMatch();

            Then_IsMatch(false);
        }
    }
}