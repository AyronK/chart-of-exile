using PathOfExile.GameClient.Monitor.Notifications.Abstraction;
using PathOfExile.GameClient.Monitor.Notifications.Metadata;

namespace PathOfExile.GameClient.Monitor.Notifications
{
    public class TextNotification : BaseNotification
    {
        public string Text { get; }

        public TextNotification(string text,  LogMetadata metadata) : base(metadata)
        {
            Text = text;
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj) && obj is TextNotification other && other.Text == Text;
        }

        public override int GetHashCode()
        {
            return Text.GetHashCode();
        }
    }
}