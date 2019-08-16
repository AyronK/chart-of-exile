using NUnit.Framework;
using PathOfExile.GameClient.Monitor.Notifications;
using PathOfExile.GameClient.Monitor.Notifications.Constants;

namespace PathOfExile.GameClient.Monitor.Tests.Given_DefaultNotificationMatching.When
{
    public class WhenChatMessageNotificationMatch : When
    {
        [TestCase("#User: Hello world!", ChatChannel.Global, "User", null, "Hello world!")]
        [TestCase("$User: Hello world!", ChatChannel.Trade, "User", null, "Hello world!")]
        [TestCase("%User: Hello world!", ChatChannel.Party, "User", null, "Hello world!")]
        [TestCase("&User: Hello world!", ChatChannel.Guild, "User", null, "Hello world!")]
        [TestCase("User: Hello world!", ChatChannel.Local, "User", null, "Hello world!")]
        [TestCase("#<Pros> User: Hello world!", ChatChannel.Global, "User", "Pros", "Hello world!")]
        [TestCase("@From User: Hello world!", ChatChannel.WhisperFrom, "User", null, "Hello world!")]
        [TestCase("@To <Pros> User: Hello world!", ChatChannel.WhisperTo, "User", "Pros", "Hello world!")]
        [TestCase("@From <Pros> User: Hello world!", ChatChannel.WhisperFrom, "User", "Pros", "Hello world!")]
        [TestCase("@To <Pros> User: Hello world!", ChatChannel.WhisperTo, "User", "Pros", "Hello world!")]
        [TestCase("&어카지: Hello world!", ChatChannel.Guild, "어카지", null, "Hello world!")]
        [TestCase("#<어카지> User: Hello world!", ChatChannel.Global, "User", "어카지", "Hello world!")]
        [TestCase("@From <Pros> 어카지: Hello world!", ChatChannel.WhisperFrom, "어카지", "Pros", "Hello world!")]
        [TestCase("@To <Pros> User: 어카지어카지!", ChatChannel.WhisperTo, "User", "Pros", "어카지어카지!")]
        [TestCase("&超級用戶: Hello world!", ChatChannel.Guild, "超級用戶", null, "Hello world!")]
        [TestCase("&الخارق: Hello world!", ChatChannel.Guild, "الخارق", null, "Hello world!")]
        [TestCase("&суперпользователя: Hello world!", ChatChannel.Guild, "суперпользователя", null, "Hello world!")]
        public void And_IsMatch(string text, ChatChannel channel, string player, string guild, string message)
        {
            With_ChatMessageNotificationMatch();

            NotificationText = $"{DefaultLogMetadataText} {text}";

            WhenIsMatch();

            Then_IsMatch();
            Then_Notification(new ChatMessageNotification(channel, player, guild, message, DefaultLogMetadata));
        }

        [TestCase("AFK mode is now OFF.", Description = "Invalid format - no ':'")]
        [TestCase(": Trade accepted.")]
        [TestCase("Connect time to instance server was 32ms")]
        public void And_IsNotMatch(string messageText)
        {
            With_ChatMessageNotificationMatch();

            NotificationText = $"{DefaultLogMetadataText} {messageText}";

            WhenIsMatch();

            Then_IsMatch(false);
        }
    }
}