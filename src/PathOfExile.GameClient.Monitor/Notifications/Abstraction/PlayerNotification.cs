using PathOfExile.GameClient.Monitor.Notifications.Metadata;

namespace PathOfExile.GameClient.Monitor.Notifications.Abstraction
{
    public abstract class PlayerNotification : BaseNotification
    {
        protected PlayerNotification(string targetNickname,  LogMetadata metadata) : base(metadata)
        {
            TargetNickname = targetNickname;
        }

        public string TargetNickname { get; }
        
        public override bool Equals(object obj)
        {
            return base.Equals(obj) && obj is PlayerNotification other && other.TargetNickname == TargetNickname;
        }

        public override int GetHashCode() //TODO clean up and fix hashcodes by introducing extension method this.Hash(a) and this.Hash(a).CombineHash(b)
        {
            return TargetNickname != null ? TargetNickname.GetHashCode() : 0;
        }
    }
}