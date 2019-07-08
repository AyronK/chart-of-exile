using NUnit.Framework;
using PathOfExile.GameClient.Monitor.Notifications;

namespace PathOfExile.GameClient.Monitor.Tests.Given_DefaultNotificationMatching.When
{
    public class WhenAreaEnterNotificationMatch : When
    {
        [Test]
        public void And_IsMatch()
        {
            With_AreaEnterNotificationMatch();

            NotificationText = $"{DefaultLogMetadataText} : You have entered Azurite Mine.";

            WhenIsMatch();

            Then_IsMatch();
            Then_Notification(new AreaChangedNotification("Azurite Mine", DefaultLogMetadata));
        }

        [TestCase("You have entered Azurite Mine.", Description = "Invalid format - no ':'")]
        [TestCase(": Trade accepted.")]
        [TestCase("Connect time to instance server was 32ms")]
        public void And_IsNotMatch(string messageText)
        {
            With_AreaEnterNotificationMatch();

            NotificationText = $"{DefaultLogMetadataText} {messageText}";

            WhenIsMatch();

            Then_IsMatch(false);
        }
    }
}