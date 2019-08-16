using PathOfExile.GameClient.Monitor.Notifications.Abstraction;
using PathOfExile.GameClient.Monitor.Notifications.Metadata;

namespace PathOfExile.GameClient.Monitor.Notifications
{
    public class KitavasAfflictionNotification : BaseNotification
    {
        public string Difficulty { get; }
        public int ResistancePenalty { get; }

        public KitavasAfflictionNotification(string difficulty, int resistancePenalty, LogMetadata metadata) : base(metadata)
        {
            Difficulty = difficulty;
            ResistancePenalty = resistancePenalty;
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj) && obj is KitavasAfflictionNotification other && string.Equals(other.Difficulty,Difficulty) && other.ResistancePenalty == ResistancePenalty;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = base.GetHashCode();
                hashCode = (hashCode * 397) ^ (Difficulty != null ? Difficulty.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ ResistancePenalty;
                return hashCode;
            }
        }
    }
}