using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using PathOfExile.GameClient.Monitor.LogTracing;
using PathOfExile.GameClient.Monitor.Matching;
using PathOfExile.GameClient.Monitor.Monitoring;

namespace PathOfExile.GameClient.Monitor.DependencyInjection
{
    public static class GameClientMonitorExtensions
    {
        private static readonly INotificationMatch[] DefaultMatchings =
        {
            DefaultNotificationMatching.AfkNotificationMatch,
            DefaultNotificationMatching.AreaEnterNotificationMatch,
            DefaultNotificationMatching.LevelUpNotificationMatch,
            DefaultNotificationMatching.ChatMessageNotificationMatch,
            DefaultNotificationMatching.PlayerInAreaNotificationMatch,
            DefaultNotificationMatching.TextNotificationMatch,
        };

        private static readonly INotificationMatch[] TradeMatchings =
        {
            DefaultNotificationMatching.TradeNotificationMatch,
            DefaultNotificationMatching.BulkTradeNotificationMatch,
            DefaultNotificationMatching.MapExchangeNotificationMatch,
        };

        public static IServiceCollection AddGameClientMonitor(this IServiceCollection services, Action<GameClientMonitorConfiguration> configure = null)
        {
            GameClientMonitorConfiguration configuration = new GameClientMonitorConfiguration();

            configure?.Invoke(configuration);

            if (configuration == null)
            {
                throw new GameClientMonitorException($"Parameter {nameof(configuration)} cannot be null");
            }

            try
            {
                LogMonitor logMonitor = new LogMonitor(configuration.ClientTxtPath);
                services.AddSingleton<ILogMonitor>(logMonitor);
            }
            catch (Exception ex)
            {
                throw new GameClientMonitorException($"Failed to initialize log monitor on client.txt file at path {configuration.ClientTxtPath}.", ex);
            }

            services.AddScoped<INotificationMonitor>(s =>
            {
                try
                {
                    return CreateNotificationMonitor(s, configuration);
                }
                catch (Exception ex)
                {
                    throw new GameClientMonitorException("Failed to initialize game client.", ex);
                }
            });

            return services;
        }

        private static NotificationMonitor CreateNotificationMonitor(IServiceProvider serviceProvider, GameClientMonitorConfiguration configuration)
        {
            ILogMonitor logMonitor = serviceProvider.GetService<ILogMonitor>();
            NotificationMonitor notificationMonitor = new NotificationMonitor(logMonitor, configuration.IsOnlyFirstMatchHandled);

            if (configuration.NotificationMatches != null)
            {
                RegisterMatchings(configuration, notificationMonitor);
            }

            if (configuration.UseTradeMatchings)
            {
                RegisterTradeMatchings(TradeMatchings, configuration, notificationMonitor);
            }

            if (configuration.UseDefaultMatchings)
            {
                RegisterTradeMatchings(DefaultMatchings, configuration, notificationMonitor);
            }

            return notificationMonitor;
        }

        private static void RegisterMatchings(GameClientMonitorConfiguration configuration, NotificationMonitor notificationMonitor)
        {
            foreach (INotificationMatch notificationMatch in configuration.NotificationMatches)
            {
                notificationMonitor.RegisterMatching(notificationMatch);
            }
        }

        private static void RegisterTradeMatchings(IEnumerable<INotificationMatch> matchings, GameClientMonitorConfiguration configuration, NotificationMonitor notificationMonitor)
        {
            foreach (INotificationMatch notificationMatch in matchings)
            {
                if (!configuration.NotificationMatches?.Contains(notificationMatch) ?? false)
                {
                    notificationMonitor.RegisterMatching(notificationMatch);
                }
            }
        }
    }
}