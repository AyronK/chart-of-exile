using NUnit.Framework;
using PathOfExile.GameClient.Monitor.Notifications;

namespace PathOfExile.GameClient.Monitor.Tests.Given_DefaultNotificationMatching.When
{
    public class WhenTradeNotificationMatch : When
    {
        [TestCase(
            "@From TestUser: Hi, I would like to buy your Underground River Map (T9) listed for 5 chisel in Legion (stash tab \"~price 5 chisel\"; position: left 1, top 1)",
            "TestUser",
            "Underground River Map",
            "T9",
            5.0,
            "chisel",
            "Legion",
            "~price 5 chisel",
            1,
            1,
            null)]
        [TestCase(
            "@From <guild> TestUser: Hi, I would like to buy your Underground River Map (T9) listed for 5 chisel in Legion (stash tab \"~price 5 chisel\"; position: left 1, top 1)",
            "TestUser",
            "Underground River Map",
            "T9",
            5.0,
            "chisel",
            "Legion",
            "~price 5 chisel",
            1,
            1,
            null)]
        [TestCase(
            "@From <수퍼유저> 수퍼유저: Hi, I would like to buy your Underground River Map (T9) listed for 5 chisel in Legion (stash tab \"~price 5 chisel\"; position: left 1, top 1)",
            "수퍼유저",
            "Underground River Map",
            "T9",
            5.0,
            "chisel",
            "Legion",
            "~price 5 chisel",
            1,
            1,
            null)]
        [TestCase(
            "@From TestUser: Hi, I would like to buy your Underground River Map (T9) listed for 5 chisel in Legion (stash tab \"~price 5 chisel\"; position: left 1, top 1) Test info",
            "TestUser",
            "Underground River Map",
            "T9",
            5.0,
            "chisel",
            "Legion",
            "~price 5 chisel",
            1,
            1,
            "Test info")]
        [TestCase(
            "@From TestUser: Hi, I would like to buy your Kaom's Heart listed for 5 chisel in Legion (stash tab \"~price 5 chisel\"; position: left 1, top 1)",
            "TestUser",
            "Kaom's Heart",
            null,
            5.0,
            "chisel",
            "Legion",
            "~price 5 chisel",
            1,
            1,
            null)]
        [TestCase(
            "@From TestUser: Hi, I would like to buy your Kaom's Heart Glorious Plate listed for 1.5 exa in Legion (stash tab \"~price 5 chisel\"; position: left 1, top 1)",
            "TestUser",
            "Kaom's Heart Glorious Plate",
            null,
            1.5,
            "exa",
            "Legion",
            "~price 5 chisel",
            1,
            1,
            null)]
        public void And_IsMatch(string text, string buyer, string item, string mapTier, double price, string currency,
            string league, string stashTab, short left, short top, string additionalInfo)
        {
            With_TradeNotificationMatch();

            NotificationText = $"{DefaultLogMetadataText} {text}";

            WhenIsMatch();

            Then_IsMatch();
            Then_Notification(new TradeNotification(buyer, item, mapTier, price, currency, league, stashTab,
                new StashTabPosition {Left = left, Top = top}, additionalInfo, DefaultLogMetadata));
        }

        [TestCase("AFK mode is now OFF.", Description = "Invalid format - no ':'")]
        [TestCase(": Trade accepted.")]
        [TestCase("Connect time to instance server was 32ms")]
        public void And_IsNotMatch(string messageText)
        {
            With_TradeNotificationMatch();

            NotificationText = $"{DefaultLogMetadataText} {messageText}";

            WhenIsMatch();

            Then_IsMatch(false);
        }
    }
}