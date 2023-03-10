using PathOfExile.GameClient.Monitor.Matching;

namespace PathOfExile.GameClient.Monitor.Tests.Given_DefaultNotificationMatching
{
    public class GivenDefaultNotificationMatching
    {
        protected INotificationMatch NotificationMatch;

        protected void With_TextNotificationMatch()
        {
            NotificationMatch = DefaultNotificationMatching.TextNotificationMatch;
        }

        protected void With_AfkNotificationMatch()
        {
            NotificationMatch = DefaultNotificationMatching.AfkNotificationMatch;
        }

        protected void With_ChatMessageNotificationMatch()
        {
            NotificationMatch = DefaultNotificationMatching.ChatMessageNotificationMatch;
        }

        protected void With_LevelUpNotificationMatch()
        {
            NotificationMatch = DefaultNotificationMatching.LevelUpNotificationMatch;
        }

        protected void With_AreaEnterNotificationMatch()
        {
            NotificationMatch = DefaultNotificationMatching.AreaEnterNotificationMatch;
        }

        protected void With_PlayerInAreaNotificationMatch()
        {
            NotificationMatch = DefaultNotificationMatching.PlayerInAreaNotificationMatch;
        }

        protected void With_KitavasAfflictionNotificationMatch()
        {
            NotificationMatch = DefaultNotificationMatching.KitavasAfflictionNotificationMatch;
        }

        protected void With_TradeNotificationMatch()
        {
            NotificationMatch = DefaultNotificationMatching.TradeNotificationMatch;
        }

        protected void With_BulkTradeNotificationMatch()
        {
            NotificationMatch = DefaultNotificationMatching.BulkTradeNotificationMatch;
        }

        protected void With_MapExchangeNotificationMatch()
        {
            NotificationMatch = DefaultNotificationMatching.MapExchangeNotificationMatch;
        }
    }
}