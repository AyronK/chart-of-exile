using NUnit.Framework;
using PathOfExile.GameClient.Monitor.Notifications;

namespace PathOfExile.GameClient.Monitor.Tests.Given_DefaultNotificationMatching.When
{
    public class WhenLevelUpNotificationMatch : When
    {
        [TestCase("TestPlayer", 5)]
        [TestCase("시원화려하게", 85)]
        public void And_IsMatch(string player, short level)
        {
            With_LevelUpNotificationMatch();

            NotificationText = $"{DefaultLogMetadataText} : {player} (Trickster) is now level {level}";

            WhenIsMatch();

            Then_IsMatch();
            Then_Notification(new LevelUpNotification(player, level, DefaultLogMetadata));
        }

        [TestCase("TestPlayer (Trickster) is now level 87", Description = "Invalid format - no ':'")]
        [TestCase(": Trade accepted.")]
        [TestCase("Connect time to instance server was 32ms")]
        public void And_IsNotMatch(string messageText)
        {
            With_LevelUpNotificationMatch();

            NotificationText = $"{DefaultLogMetadataText} {messageText}";

            WhenIsMatch();

            Then_IsMatch(false);
        }
    }
}