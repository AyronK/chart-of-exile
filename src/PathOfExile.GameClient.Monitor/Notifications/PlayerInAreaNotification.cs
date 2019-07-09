using System;
using System.Collections.Generic;
using System.Text;
using PathOfExile.GameClient.Monitor.Notifications.Abstraction;
using PathOfExile.GameClient.Monitor.Notifications.Constants;
using PathOfExile.GameClient.Monitor.Notifications.Metadata;

namespace PathOfExile.GameClient.Monitor.Notifications
{
    public class PlayerInAreaNotification : BaseNotification
    {
        public string Player { get; }
        public PlayerAreaAction Action { get; }

        public PlayerInAreaNotification(string player, PlayerAreaAction action, LogMetadata metadata) : base(metadata)
        {
            Player = player;
            Action = action;
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj) && obj is PlayerInAreaNotification other && other.Player == Player && other.Action == Action;
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = base.GetHashCode();
                hashCode = (hashCode * 397) ^ (Player != null ? Player.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (int) Action;
                return hashCode;
            }
        }
    }
}
