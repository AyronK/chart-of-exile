using PathOfExile.GameClient.Monitor.Notifications.Abstraction;
using PathOfExile.GameClient.Monitor.Notifications.Metadata;

namespace PathOfExile.GameClient.Monitor.Notifications
{
    public class TradeNotification : BaseNotification
    {
        public TradeNotification(string buyer, string item, string mapTier, double price, string currency, string league, string stashTab, StashTabPosition position, string additionalMessage, LogMetadata metadata) : base(metadata)
        {
            Buyer = buyer;
            Quantity = 1;
            Item = item;
            MapTier = string.IsNullOrWhiteSpace(mapTier) ? null : mapTier;
            Price = price;
            Currency = currency;
            League = league;
            StashTab = stashTab;
            Position = position;
            AdditionalMessage = string.IsNullOrWhiteSpace(additionalMessage) ? null : additionalMessage;
        }

        public TradeNotification(string buyer, string item, string mapTier, double price, string currency, string league, string stashTab, StashTabPosition position, LogMetadata metadata) : base(metadata)
        {
            Buyer = buyer;
            Quantity = 1;
            Item = item;
            MapTier = string.IsNullOrWhiteSpace(mapTier) ? null : mapTier;
            Price = price;
            Currency = currency;
            League = league;
            StashTab = stashTab;
            Position = position;
            AdditionalMessage = null;
        }

        public TradeNotification(string buyer, short quantity, string item, string mapTier, double price, string currency, string league, LogMetadata metadata) : base(metadata)
        {
            Buyer = buyer;
            Quantity = quantity;
            Item = item;
            MapTier = string.IsNullOrWhiteSpace(mapTier) ? null : mapTier;
            Price = price;
            Currency = currency;
            League = league;
            StashTab = null;
            Position = null;
            AdditionalMessage = null;
        }

        public TradeNotification(string buyer, short quantity, string item, string mapTier, double price, string currency, string league, string additionalMessage, LogMetadata metadata) : base(metadata)
        {
            Buyer = buyer;
            Quantity = quantity;
            Item = item;
            MapTier = string.IsNullOrWhiteSpace(mapTier) ? null : mapTier;
            Price = price;
            Currency = currency;
            League = league;
            StashTab = null;
            Position = null;
            AdditionalMessage = string.IsNullOrWhiteSpace(additionalMessage) ? null : additionalMessage;
        }

        public string Buyer { get; set; }

        public string Item { get; set; }

        public string StashTab { get; set; }

        public StashTabPosition? Position { get; set; }

        public double Price { get; set; }

        public string Currency { get; set; }

        public string League { get; set; }

        public short Quantity { get; set; }

        public string AdditionalMessage { get; set; }

        public string MapTier { get; set; }

        public override bool Equals(object obj)
        {
            return base.Equals(obj) && obj is TradeNotification other && string.Equals(MapTier, other.MapTier) && string.Equals(Buyer, other.Buyer) && string.Equals(Item, other.Item) && string.Equals(StashTab, other.StashTab) && Position.Equals(other.Position) && Price.Equals(other.Price) && string.Equals(Currency, other.Currency) && string.Equals(League, other.League) && Quantity == other.Quantity && string.Equals(AdditionalMessage, other.AdditionalMessage);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = base.GetHashCode();
                hashCode = (hashCode * 397) ^ (Buyer != null ? Buyer.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Item != null ? Item.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (StashTab != null ? StashTab.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ Position.GetHashCode();
                hashCode = (hashCode * 397) ^ Price.GetHashCode();
                hashCode = (hashCode * 397) ^ (Currency != null ? Currency.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (League != null ? League.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ Quantity.GetHashCode();
                hashCode = (hashCode * 397) ^ MapTier.GetHashCode();
                hashCode = (hashCode * 397) ^ (AdditionalMessage != null ? AdditionalMessage.GetHashCode() : 0);
                return hashCode;
            }
        }

    }
}