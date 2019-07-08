using System;
using System.IO;
using System.Timers;

namespace PathOfExile.GameClient.Monitor.LogTracing
{
    internal class LogMonitor : ILogMonitor, IDisposable
    {
        private readonly StreamReader logReader;
        readonly FileStream fileStream;
        private readonly Timer checkTimer;
        private const int RefreshRateInMilliseconds = 1000;

        public LogMonitor(string clientLogPath)
        {
            if (!File.Exists(clientLogPath))
            {
                throw new ArgumentException($"File '{clientLogPath}' does not exist.", nameof(clientLogPath));
            }

            fileStream = new FileStream(clientLogPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            fileStream.Position = fileStream.Length;

            logReader = new StreamReader(fileStream);

            checkTimer = new Timer(RefreshRateInMilliseconds);
            checkTimer.Elapsed += ReadLog;
            checkTimer.Start();
        }

        private void ReadLog(object sender, ElapsedEventArgs e)
        {
            checkTimer.Stop();
            
            while (logReader.ReadLine() is string newLine)
            {
                OnEntryCreated(newLine); // TODO add concatenation until new log line is met (with timestamp) to avoid data loss on multiline log entries
            }

            checkTimer.Start();
        }

        public void Dispose()
        {
            fileStream.Dispose();
            logReader.Dispose();
            checkTimer.Dispose();
        }

        public event EventHandler<EntryCreatedEventArgs> EntryCreated;

        protected virtual void OnEntryCreated(string value)
        {
            EntryCreated?.Invoke(this, new EntryCreatedEventArgs(value));
        }
    }
}
