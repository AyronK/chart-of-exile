using PathOfExile.GameClient.Monitor.Notifications.Abstraction;
using PathOfExile.GameClient.Monitor.Notifications.Metadata;

namespace PathOfExile.GameClient.Monitor.Notifications
{
    public class AreaChangedNotification : BaseNotification
    {
        public AreaChangedNotification(string area, LogMetadata metadata) : base(metadata)
        {
            Area = area;
        }

        public string Area { get; }

        public override bool Equals(object obj)
        {
            return base.Equals(obj) && obj is AreaChangedNotification other && other.Area == Area;
        }

        public override int GetHashCode()
        {
            return Area != null ? Area.GetHashCode() : 0;
        }
    }
}