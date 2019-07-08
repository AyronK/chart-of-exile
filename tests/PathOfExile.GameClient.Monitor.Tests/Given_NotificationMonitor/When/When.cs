using System;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using PathOfExile.GameClient.Monitor.LogTracing;
using PathOfExile.GameClient.Monitor.Matching;
using PathOfExile.GameClient.Monitor.Notifications;
using PathOfExile.GameClient.Monitor.Notifications.Abstraction;
using PathOfExile.GameClient.Monitor.Notifications.Metadata;

namespace PathOfExile.GameClient.Monitor.Tests.Given_NotificationMonitor.When
{
    internal class When : GivenNotificationMonitor
    {
        private readonly LogMetadata defaultLogMetadata = new LogMetadata(new DateTime(2019, 06, 12, 21, 45, 30), LogLevel.Info);
        private readonly string defaultLogMetadataText = "2019/06/12 21:45:30 3575671 ce8 [INFO Client 6616]";
        private List<string> logEntryLines;
        private List<INotification> notifications;

        [SetUp]
        public override void SetUp()
        {
            base.SetUp();

            notifications = new List<INotification>();
            logEntryLines = new List<string>();
        }

        private void When_LogLinesProcessed()
        {
            foreach (var line in logEntryLines)
            {
                LogMonitorMock.Raise(l => l.EntryCreated += null, new EntryCreatedEventArgs(line));
            }
        }

        [Test]
        public void And_OneNotification_TextNotification()
        {
            With_NotificationMonitor();
            With_NotificationReceived();

            With_NotificationMatch(DefaultNotificationMatching.TextNotificationMatch);

            var text = "Got Instance Details from login server";
            logEntryLines = new List<string>
            {
                $"{defaultLogMetadataText} {text}"
            };

            When_LogLinesProcessed();

            Then_Notification(0, new TextNotification(text, defaultLogMetadata));
        }

        [Test]
        public void And_TwoMatchingsRegisteredOneTriggered()
        {
            With_NotificationMonitor();
            With_NotificationReceived();

            With_NotificationMatch(DefaultNotificationMatching.AfkNotificationMatch);
            With_NotificationMatch(DefaultNotificationMatching.LevelUpNotificationMatch);

            logEntryLines = new List<string>
            {
                $"{defaultLogMetadataText} : AFK mode is now ON. Autoreply \"This player is AFK.\""
            };

            When_LogLinesProcessed();

            Then_Notification(0, new AfkNotification(true, defaultLogMetadata));
        }

        [Test]
        public void And_TwoMatchingsRegisteredTwoTriggered_AndProperOrder()
        {
            With_NotificationMonitor();
            With_NotificationReceived();

            With_NotificationMatch(DefaultNotificationMatching.AfkNotificationMatch);
            With_NotificationMatch(DefaultNotificationMatching.LevelUpNotificationMatch);

            logEntryLines = new List<string>
            {
                $"{defaultLogMetadataText} : TestPlayer (Trickster) is now level 89",
                $"{defaultLogMetadataText} : AFK mode is now ON. Autoreply \"This player is AFK.\""
            };

            When_LogLinesProcessed();

            Then_Notification(0, new LevelUpNotification("TestPlayer", 89, defaultLogMetadata));
            Then_Notification(1, new AfkNotification(true, defaultLogMetadata));
        }

        [Test]
        public void And_TwoMatchingsRegisteredTwoTriggered_AndProperOrder_AndOrderDiffersFromRegistration()
        {
            With_NotificationMonitor();
            With_NotificationReceived();

            With_NotificationMatch(DefaultNotificationMatching.LevelUpNotificationMatch);
            With_NotificationMatch(DefaultNotificationMatching.AfkNotificationMatch);

            logEntryLines = new List<string>
            {
                $"{defaultLogMetadataText} : TestPlayer (Trickster) is now level 89",
                $"{defaultLogMetadataText} : AFK mode is now ON. Autoreply \"This player is AFK.\""
            };

            When_LogLinesProcessed();

            Then_Notification(0, new LevelUpNotification("TestPlayer", 89, defaultLogMetadata));
            Then_Notification(1, new AfkNotification(true, defaultLogMetadata));
        }

        [Test]
        public void And_TwoCoincidentMatchings_AndCorrectOrder()
        {
            With_NotificationMonitor();
            With_NotificationReceived();

            With_NotificationMatch(DefaultNotificationMatching.TextNotificationMatch);
            With_NotificationMatch(DefaultNotificationMatching.AfkNotificationMatch);

            var text = ": AFK mode is now ON. Autoreply \"This player is AFK.\"";
            logEntryLines = new List<string>
            {
                $"{defaultLogMetadataText} {text}"
            };

            When_LogLinesProcessed();

            Then_Notification(0, new TextNotification(text, defaultLogMetadata));
            Then_Notification(1, new AfkNotification(true, defaultLogMetadata));
        }

        [Test]
        public void And_TwoCoincidentMatchings_AndCorrectOrder_InverseCase()
        {
            With_NotificationMonitor();
            With_NotificationReceived();

            With_NotificationMatch(DefaultNotificationMatching.AfkNotificationMatch);
            With_NotificationMatch(DefaultNotificationMatching.TextNotificationMatch);

            var text = ": AFK mode is now ON. Autoreply \"This player is AFK.\"";
            logEntryLines = new List<string>
            {
                $"{defaultLogMetadataText} {text}"
            };

            When_LogLinesProcessed();

            Then_Notification(0, new AfkNotification(true, defaultLogMetadata));
            Then_Notification(1, new TextNotification(text, defaultLogMetadata));
        }

        [Test(Description = "Multiple notification matchings with entry line for all and with overlapping cases.")]
        public void And_ComplexCase_One()
        {
            With_NotificationMonitor();
            With_NotificationReceived();

            With_NotificationMatch(DefaultNotificationMatching.LevelUpNotificationMatch);
            With_NotificationMatch(DefaultNotificationMatching.AfkNotificationMatch);
            With_NotificationMatch(DefaultNotificationMatching.TextNotificationMatch);

            logEntryLines = new List<string>
            {
                $"{defaultLogMetadataText} : TestPlayer (Trickster) is now level 89",
                $"{defaultLogMetadataText} : AFK mode is now ON. Autoreply \"This player is AFK.\""
            };

            When_LogLinesProcessed();

            Then_Notification(0, new LevelUpNotification("TestPlayer", 89, defaultLogMetadata));
            Then_Notification(1, new TextNotification(": TestPlayer (Trickster) is now level 89", defaultLogMetadata));
            Then_Notification(2, new AfkNotification(true, defaultLogMetadata));
            Then_Notification(3, new TextNotification(": AFK mode is now ON. Autoreply \"This player is AFK.\"", defaultLogMetadata));
        }

        [Test(Description = "Multiple notification matchings with entry line for not every one and with overlapping cases.")]
        public void And_ComplexCase_Two()
        {
            With_NotificationMonitor();
            With_NotificationReceived();

            With_NotificationMatch(DefaultNotificationMatching.LevelUpNotificationMatch);
            With_NotificationMatch(DefaultNotificationMatching.AfkNotificationMatch);
            With_NotificationMatch(DefaultNotificationMatching.AreaEnterNotificationMatch);
            With_NotificationMatch(DefaultNotificationMatching.TextNotificationMatch);

            logEntryLines = new List<string>
            {
                $"{defaultLogMetadataText} : TestPlayer (Trickster) is now level 89",
                $"{defaultLogMetadataText} : AFK mode is now ON. Autoreply \"This player is AFK.\""
            };

            When_LogLinesProcessed();

            Then_Notification(0, new LevelUpNotification("TestPlayer", 89, defaultLogMetadata));
            Then_Notification(1, new TextNotification(": TestPlayer (Trickster) is now level 89", defaultLogMetadata));
            Then_Notification(2, new AfkNotification(true, defaultLogMetadata));
            Then_Notification(3, new TextNotification(": AFK mode is now ON. Autoreply \"This player is AFK.\"", defaultLogMetadata));
        }



        [Test(Description = "Multiple notification matchings with entry line for all and with overlapping cases. Only first match should be handled.")]
        public void And_ComplexCase_Three()
        {
            With_NotificationMonitor_HandlingOnlyFirstMatch();
            With_NotificationReceived();

            With_NotificationMatch(DefaultNotificationMatching.LevelUpNotificationMatch);
            With_NotificationMatch(DefaultNotificationMatching.AfkNotificationMatch);
            With_NotificationMatch(DefaultNotificationMatching.TextNotificationMatch);

            logEntryLines = new List<string>
            {
                $"{defaultLogMetadataText} : TestPlayer (Trickster) is now level 89",
                $"{defaultLogMetadataText} : AFK mode is now ON. Autoreply \"This player is AFK.\""
            };

            When_LogLinesProcessed();

            Then_Notification(0, new LevelUpNotification("TestPlayer", 89, defaultLogMetadata));
            Then_Notification(1, new TextNotification(": TestPlayer (Trickster) is now level 89", defaultLogMetadata));
            Then_Notification(2, new AfkNotification(true, defaultLogMetadata));
            Then_Notification(3, new TextNotification(": AFK mode is now ON. Autoreply \"This player is AFK.\"", defaultLogMetadata));
        }

        [Test(Description = "Multiple notification matchings with entry line for not every one and with overlapping cases. Only first match should be handled.")]
        public void And_ComplexCase_Four()
        {
            With_NotificationMonitor_HandlingOnlyFirstMatch();
            With_NotificationReceived();

            With_NotificationMatch(DefaultNotificationMatching.LevelUpNotificationMatch);
            With_NotificationMatch(DefaultNotificationMatching.AfkNotificationMatch);
            With_NotificationMatch(DefaultNotificationMatching.AreaEnterNotificationMatch);
            With_NotificationMatch(DefaultNotificationMatching.TextNotificationMatch);

            logEntryLines = new List<string>
            {
                $"{defaultLogMetadataText} : TestPlayer (Trickster) is now level 89",
                $"{defaultLogMetadataText} : AFK mode is now ON. Autoreply \"This player is AFK.\""
            };

            When_LogLinesProcessed();

            Then_Notification(0, new LevelUpNotification("TestPlayer", 89, defaultLogMetadata));
            Then_Notification(1, new TextNotification(": TestPlayer (Trickster) is now level 89", defaultLogMetadata));
            Then_Notification(2, new AfkNotification(true, defaultLogMetadata));
            Then_Notification(3, new TextNotification(": AFK mode is now ON. Autoreply \"This player is AFK.\"", defaultLogMetadata));
        }

        [Test]
        public void And_RegisteredTwoSameMatchings()
        {
            try
            {
                With_NotificationMatch(DefaultNotificationMatching.LevelUpNotificationMatch);
                With_NotificationMatch(DefaultNotificationMatching.LevelUpNotificationMatch);
            }
            catch (Exception exception)
            {
                exception.Should().NotBeNull();
                exception.Should().BeOfType<ArgumentException>();
            }
        }

        private void With_NotificationReceived()
        {
            Monitor.NotificationReceived += (sender, args) => { notifications.Add(args.Notification); };
        }

        private void Then_Notification(int fireOrderIndex, INotification expectedNotification)
        {
            notifications.Should().HaveCountGreaterOrEqualTo(fireOrderIndex + 1);
            notifications[fireOrderIndex].Should().NotBeNull();
            notifications[fireOrderIndex].Should().Be(expectedNotification);
        }
    }
}