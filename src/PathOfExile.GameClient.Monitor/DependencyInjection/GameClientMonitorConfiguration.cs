using System;
using System.IO;
using PathOfExile.GameClient.Monitor.Matching;

namespace PathOfExile.GameClient.Monitor.DependencyInjection
{
    public class GameClientMonitorConfiguration
    {
        public bool UseDefaultMatchings { get; set; } = true;
        public bool UseTradeMatchings { get; set; } = false;
        public string ClientTxtPath { get; set; } = Path.Combine(Environment.ExpandEnvironmentVariables("%ProgramW6432%"), @"\Steam\steamapps\common\Path of Exile\logs\Client.txt");
        public INotificationMatch[] NotificationMatches { get; set; } = new INotificationMatch[0];
        public bool IsOnlyFirstMatchHandled { get; set; } = true;
    }
}