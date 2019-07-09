using PathOfExile.GameClient.Monitor.Notifications.Abstraction;
using PathOfExile.GameClient.Monitor.Notifications.Metadata;

namespace PathOfExile.GameClient.Monitor.Notifications
{
    public class LevelUpNotification : BaseNotification
    {
        public LevelUpNotification(string player, short level,  LogMetadata metadata) : base(metadata)
        {
            Player = player;
            Level = level;
        }

        public string Player { get; }
        public short Level { get; }

        public override bool Equals(object obj)
        {
            return base.Equals(obj) && obj is LevelUpNotification other && other.Level == Level && other.Player == Player;
        }

        public override int GetHashCode()
        {
            return Level.GetHashCode();
        }
    }
}