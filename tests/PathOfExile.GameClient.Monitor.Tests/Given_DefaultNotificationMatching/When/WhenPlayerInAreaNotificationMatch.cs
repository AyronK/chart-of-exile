using NUnit.Framework;
using PathOfExile.GameClient.Monitor.Notifications;

namespace PathOfExile.GameClient.Monitor.Tests.Given_DefaultNotificationMatching.When
{
    public class WhenPlayerInAreaNotificationMatch : When
    {
        [TestCase(": User has joined the area", "User", PlayerAreaAction.Join)]
        [TestCase(": User has left the area", "User", PlayerAreaAction.Leave)]
        [TestCase(": 수퍼유저 has joined the area", "수퍼유저", PlayerAreaAction.Join)]
        [TestCase(": 수퍼유저 has left the area", "수퍼유저", PlayerAreaAction.Leave)]
        public void And_IsMatch(string text, string player, PlayerAreaAction action)
        {
            With_PlayerInAreaNotificationMatch();

            NotificationText = $"{DefaultLogMetadataText} {text}.";

            WhenIsMatch();

            Then_IsMatch();
            Then_Notification(new PlayerInAreaNotification(player, action, DefaultLogMetadata));
        }

        [TestCase("AFK mode is now OFF.", Description = "Invalid format - no ':'")]
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