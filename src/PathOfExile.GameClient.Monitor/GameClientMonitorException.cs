using System;

namespace PathOfExile.GameClient.Monitor
{
    public sealed class GameClientMonitorException : Exception
    {
        public GameClientMonitorException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public GameClientMonitorException(string message) : base(message)
        {
        }
    }
}