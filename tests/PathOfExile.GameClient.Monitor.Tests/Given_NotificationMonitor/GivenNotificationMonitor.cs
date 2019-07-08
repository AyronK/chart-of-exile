using Moq;
using NUnit.Framework;
using PathOfExile.GameClient.Monitor.LogTracing;
using PathOfExile.GameClient.Monitor.Matching;
using PathOfExile.GameClient.Monitor.Monitoring;

namespace PathOfExile.GameClient.Monitor.Tests.Given_NotificationMonitor
{
    internal class GivenNotificationMonitor
    {
        protected Mock<ILogMonitor> LogMonitorMock;
        protected NotificationMonitor Monitor;

        [SetUp]
        public virtual void SetUp()
        {
            LogMonitorMock = new Mock<ILogMonitor>(MockBehavior.Strict);
        }

        protected void With_NotificationMonitor()
        {
            Monitor = new NotificationMonitor(LogMonitorMock.Object);
        }

        protected void With_NotificationMonitor_HandlingOnlyFirstMatch()
        {
            Monitor = new NotificationMonitor(LogMonitorMock.Object);
        }

        protected void With_NotificationMatch(INotificationMatch notificationMatch)
        {
            Monitor.RegisterMatching(notificationMatch);
        }
    }
}