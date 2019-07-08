using PathOfExile.GameClient.Monitor.Notifications.Abstraction;
using PathOfExile.GameClient.Monitor.Notifications.Metadata;

namespace PathOfExile.GameClient.Monitor.Notifications
{
    public class AfkNotification : BaseNotification // TODO add DND mode support
    {
        public bool IsActive { get; }

        public AfkNotification(bool isActive,  LogMetadata metadata) : base(metadata)
        {
            IsActive = isActive;
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj) && obj is AfkNotification other && other.IsActive == IsActive;
        }

        public override int GetHashCode()
        {
            return IsActive.GetHashCode();
        }
    }
}