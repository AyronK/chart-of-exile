using PathOfExile.GameClient.Monitor.Notifications.Abstraction;
using PathOfExile.GameClient.Monitor.Notifications.Metadata;

namespace PathOfExile.GameClient.Monitor.Notifications
{
    public class MapExchangeNotification : BaseNotification
    {
        public string Buyer { get; set; }

        public string BuyerMapTier { get; set; }

        public string[] BuyerMaps { get; set; }

        public string YourMapTier { get; set; }

        public string[] YourMaps { get; set; }

        public string League { get; set; }

        public string AdditionalMessage { get; set; }

        public MapExchangeNotification(string buyer, string buyerMapTier, string[] buyerMaps, string yourMapTier, string[] yourMaps, string league, string additionalMessage, LogMetadata metadata) : base(metadata)
        {
            Buyer = buyer;
            BuyerMapTier = buyerMapTier;
            BuyerMaps = buyerMaps;
            YourMapTier = yourMapTier;
            YourMaps = yourMaps;
            League = league;
            AdditionalMessage = additionalMessage;
        }
    }
}