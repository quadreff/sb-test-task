using System;
using System.Diagnostics;

namespace SBTestTask.Common.Logging
{
    public static class Log
    {
        private static Action<LogSeverity, string>? _logAction;

        public static void Setup(Action<LogSeverity, string> logAction)
        {
            _logAction = logAction;
        }

        public static void Trace(string message)
        {
            Write(LogSeverity.Trace, message);
        }

        public static void Info(string message)
        {
            Write(LogSeverity.Info, message);
        }

        public static void Error(string message)
        {
            Write(LogSeverity.Error, message);
        }

        private static void Write(LogSeverity severity, string message)
        {
            try
            {
                _logAction?.Invoke(severity, message);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }
    }
}