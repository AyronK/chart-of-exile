using NUnit.Framework;
using PathOfExile.GameClient.Monitor.Notifications;

namespace PathOfExile.GameClient.Monitor.Tests.Given_DefaultNotificationMatching.When
{
    public class WhenBulkTradeNotificationMatch : When
    {
        [TestCase(
            "@From TestUser: Hi, I'd like to buy your 5 Dense Fossil for my 10 Chaos Orb in Legion.",
            "TestUser",
            5,
            "Dense Fossil",
            null,
            10.0,
            "Chaos Orb",
            "Legion",
            null)]
        [TestCase(
            "@From <guild> TestUser: Hi, I'd like to buy your 5 Dense Fossil for my 10 Chaos Orb in Legion.",
            "TestUser",
            5,
            "Dense Fossil",
            null,
            10.0,
            "Chaos Orb",
            "Legion",
            null)]
        [TestCase(
            "@From <guild> TestUser: Hi, I'd like to buy your 5 Dense Fossil for my 10 Chaos Orb in Legion. Test info",
            "TestUser",
            5,
            "Dense Fossil",
            null,
            10.0,
            "Chaos Orb",
            "Legion",
            "Test info")]
        [TestCase(
            "@From <수퍼유저> 수퍼유저: Hi, I'd like to buy your 5 Dense Fossil for my 10 Chaos Orb in Legion.",
            "수퍼유저",
            5,
            "Dense Fossil",
            null,
            10.0,
            "Chaos Orb",
            "Legion",
            null)]
        [TestCase(
            "@From TestUser: Hi, I'd like to buy your 5 Underground River Map (T9) for my 10 Chaos Orb in Legion.",
            "TestUser",
            5,
            "Underground River Map",
            "T9",
            10.0,
            "Chaos Orb",
            "Legion",
            null)]
        public void And_IsMatch(string text, string buyer, short quantity, string item, string mapTier, double price, string currency,
            string league, string additionalInfo)
        {
            With_BulkTradeNotificationMatch();

            NotificationText = $"{DefaultLogMetadataText} {text}";

            WhenIsMatch();

            Then_IsMatch();
            Then_Notification(new TradeNotification(buyer, quantity, item, mapTier, price, currency, league, additionalInfo, DefaultLogMetadata));
        }

        [TestCase("AFK mode is now OFF.", Description = "Invalid format - no ':'")]
        [TestCase(": Trade accepted.")]
        [TestCase("Connect time to instance server was 32ms")]
        public void And_IsNotMatch(string messageText)
        {
            With_BulkTradeNotificationMatch();

            NotificationText = $"{DefaultLogMetadataText} {messageText}";

            WhenIsMatch();

            Then_IsMatch(false);
        }
    }
}