using System;
using log4net;

namespace GiveCRM.LoggingService
{
    public class LogService:ILogService
    {
        private readonly ILog logger;

        public LogService()
        {
            log4net.Config.XmlConfigurator.Configure();
            logger = LogManager.GetLogger(GetType());
        }

        public void LogInformation(string message, Exception exception = null)
        {
            if(logger.IsInfoEnabled)
            {
                if (exception == null)
                {
                    logger.Info(message);
                }
                else
                {
                    logger.Info(message,exception);
                }
            }
        }

        public void LogDebug(string message, Exception exception = null)
        {
            if(logger.IsDebugEnabled)
            {
                if (exception == null)
                {
                    logger.Debug(message);
                }
                else
                {
                    logger.Debug(message, exception);
                }
            }
        }

        public void LogWarning(string message, Exception exception = null)
        {
            if(logger.IsWarnEnabled)
            {
                if (exception == null)
                {
                    logger.Warn(message);
                }
                else
                {
                    logger.Warn(message, exception);
                }
            }
        }

        public void LogError(string message, Exception exception = null)
        {
            if(logger.IsErrorEnabled)
            {
                if (exception == null)
                {
                    logger.Error(message);
                }
                else
                {
                    logger.Error(message, exception);
                }
            }
        }

        public void LogException(string message, Exception exception = null)
        {
            if(logger.IsFatalEnabled)
            {
                if (exception == null)
                {
                    logger.Fatal(message);
                }
                else
                {
                    logger.Fatal(message, exception);
                };
            }
        }
    }
}