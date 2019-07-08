using System;
using NUnit.Framework;
using PathOfExile.GameClient.Monitor.Notifications;

namespace PathOfExile.GameClient.Monitor.Tests.Given_DefaultNotificationMatching.When
{
    public class WhenTextNotificationMatch : When
    {
        [Test]
        public void And_IsMatch()
        {
            With_TextNotificationMatch();

            string text = Guid.NewGuid().ToString();
            NotificationText = $"{DefaultLogMetadataText} {text}";

            WhenIsMatch();

            Then_IsMatch();
            Then_Notification(new TextNotification(text, DefaultLogMetadata));
        }
    }
}