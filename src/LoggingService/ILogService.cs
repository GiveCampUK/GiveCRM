namespace GiveCRM.LoggingService
{
    using System;

    public interface ILogService
    {
        
        void LogInformation(string message,Exception exception = null);
        void LogDebug(string message, Exception exception = null);
        void LogWarning(string message, Exception exception = null);
        void LogError(string message, Exception exception = null);
        void LogException(string message, Exception exception = null);
    }
}
