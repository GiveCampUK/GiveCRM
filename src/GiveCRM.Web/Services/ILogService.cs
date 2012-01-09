namespace GiveCRM.Web.Services
{
    public interface ILogService
    {
        void LogInformation(string message);
        void LogDebug(string message);
        void LogWarning(string message);
        void LogError(string message);
        void LogException(string message);
    }
}