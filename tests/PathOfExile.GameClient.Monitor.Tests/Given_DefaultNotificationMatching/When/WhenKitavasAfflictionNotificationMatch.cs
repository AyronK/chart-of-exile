using NUnit.Framework;
using PathOfExile.GameClient.Monitor.Notifications;

namespace PathOfExile.GameClient.Monitor.Tests.Given_DefaultNotificationMatching.When
{
    public class WhenKitavasAfflictionNotificationMatch : When
    {
        [TestCase(": You have been permanently weakened by Kitava's cruel affliction. You now have -30% to all Resistances.", "cruel", 30)]
        [TestCase(": You have been permanently weakened by Kitava's merciless affliction. You now have -60% to all Resistances.", "merciless", 60)]
        public void And_IsMatch(string text, string difficulty, int penalty)
        {
            With_KitavasAfflictionNotificationMatch();

            NotificationText = $"{DefaultLogMetadataText} {text}.";

            WhenIsMatch();

            Then_IsMatch();
            Then_Notification(new KitavasAfflictionNotification(difficulty, penalty, DefaultLogMetadata));
        }

        [TestCase("AFK mode is now OFF.", Description = "Invalid format - no ':'")]
        [TestCase(": Trade accepted.")]
        [TestCase("Connect time to instance server was 32ms")]
        public void And_IsNotMatch(string messageText)
        {
            With_KitavasAfflictionNotificationMatch();

            NotificationText = $"{DefaultLogMetadataText} {messageText}";

            WhenIsMatch();

            Then_IsMatch(false);
        }
    }
}