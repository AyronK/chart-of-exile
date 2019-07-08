using PathOfExile.GameClient.Monitor.Notifications.Abstraction;
using PathOfExile.GameClient.Monitor.Notifications.Metadata;

namespace PathOfExile.GameClient.Monitor.Notifications
{
    public class LevelUpNotification : PlayerNotification
    {
        public LevelUpNotification(string targetNickname, short level,  LogMetadata metadata) : base(targetNickname, metadata)
        {
            Level = level;
        }

        public short Level { get; }

        public override bool Equals(object obj)
        {
            return base.Equals(obj) && obj is LevelUpNotification other && other.Level == Level;
        }

        public override int GetHashCode()
        {
            return Level.GetHashCode();
        }
    }
}