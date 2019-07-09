using PathOfExile.GameClient.Monitor.Notifications.Abstraction;
using PathOfExile.GameClient.Monitor.Notifications.Metadata;

namespace PathOfExile.GameClient.Monitor.Notifications
{
    public enum ChatChannel
    {
        Local,
        Global,
        Trade,
        Guild,
        WhisperTo,
        WhisperFrom,
        Party
    }

    public sealed class ChatMessageNotification : BaseNotification
    {
        public string Player { get; }

        public string Guild { get; }

        public ChatChannel Channel { get; }

        public string Message { get; }

        public ChatMessageNotification(ChatChannel channel, string player, string guild, string message, LogMetadata metadata) : base(metadata)
        {
            Channel = channel;
            Player = player;
            Guild = string.IsNullOrWhiteSpace(guild) ? null : guild;
            Message = message;
        }

        public ChatMessageNotification(ChatChannel channel, string player, string message, LogMetadata metadata) : base(metadata)
        {
            Channel = channel;
            Player = player;
            Message = message;
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj) && obj is ChatMessageNotification other && other.Player == Player && other.Guild == Guild && other.Channel == Channel && other.Message == Message;
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = base.GetHashCode();
                hashCode = (hashCode * 397) ^ (Player != null ? Player.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Guild != null ? Guild.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (int) Channel;
                hashCode = (hashCode * 397) ^ (Message != null ? Message.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}