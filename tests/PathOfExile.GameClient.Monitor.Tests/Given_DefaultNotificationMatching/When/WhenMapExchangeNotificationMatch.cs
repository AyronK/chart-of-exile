using NUnit.Framework;
using PathOfExile.GameClient.Monitor.Notifications;

namespace PathOfExile.GameClient.Monitor.Tests.Given_DefaultNotificationMatching.When
{
    public class WhenMapExchangeNotificationMatch : When
    {
        [TestCase(
            "@From TestUser: I'd like to exchange my T10: (Bazaar) for your T9: (Lava Chamber) in Legion.",
            "TestUser",
            "T10",
            "Bazaar",
            "T9",
            "Lava Chamber",
            "Legion",
            null)]
        [TestCase(
            "@From TestUser: I'd like to exchange my T10: (Bazaar) for your T9: (Lava Chamber) in Legion. Test info",
            "TestUser",
            "T10",
            "Bazaar",
            "T9",
            "Lava Chamber",
            "Legion",
            "Test info")]
        [TestCase(
            "@From <guild> TestUser: I'd like to exchange my T10: (Bazaar) for your T9: (Lava Chamber) in Legion.",
            "TestUser",
            "T10",
            "Bazaar",
            "T9",
            "Lava Chamber",
            "Legion",
            null)]
        [TestCase(
            "@From <수퍼유저> 수퍼유저: I'd like to exchange my T10: (Bazaar) for your T9: (Lava Chamber) in Legion.",
            "수퍼유저",
            "T10",
            "Bazaar",
            "T9",
            "Lava Chamber",
            "Legion",
            null)]
        [TestCase(
            "@From TestUser: I'd like to exchange my T10: (Bazaar) for your T9: (Lava Chamber) in Legion.",
            "TestUser",
            "T10",
            "Factory,Pit",
            "T9",
            "Chateau,Estuary,Scriptorium,Necropolis",
            "Legion",
            null)]
        public void And_IsMatch(string text, string buyer, string buyerMapTier, string buyerMaps, string yourMapTier, string yourMaps, string league, string additionalInfo)
        {
            With_MapExchangeNotificationMatch();

            NotificationText = $"{DefaultLogMetadataText} {text}";

            WhenIsMatch();

            Then_IsMatch();
            Then_Notification(new MapExchangeNotification(buyer, buyerMapTier, buyerMaps.Split(","), yourMapTier, yourMaps.Split(","), league, additionalInfo, DefaultLogMetadata));
        }

        [TestCase("AFK mode is now OFF.", Description = "Invalid format - no ':'")]
        [TestCase(": Trade accepted.")]
        [TestCase("Connect time to instance server was 32ms")]
        public void And_IsNotMatch(string messageText)
        {
            With_MapExchangeNotificationMatch();

            NotificationText = $"{DefaultLogMetadataText} {messageText}";

            WhenIsMatch();

            Then_IsMatch(false);
        }
    }
}