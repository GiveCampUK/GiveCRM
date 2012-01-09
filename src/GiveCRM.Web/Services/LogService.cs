using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using log4net;

namespace GiveCRM.Web.Services
{
    public class LogService:ILogService
    {
        private readonly ILog logger;

        public LogService()
        {
            log4net.Config.XmlConfigurator.Configure();
            logger = LogManager.GetLogger(GetType());
        }

        public void LogInformation(string message)
        {
            if(logger.IsInfoEnabled)
            {
                logger.Info(message);
            }
        }

        public void LogDebug(string message)
        {
            if(logger.IsDebugEnabled)
            {
                logger.Debug(message);
            }
        }

        public void LogWarning(string message)
        {
            if(logger.IsWarnEnabled)
            {
                logger.Warn(message);
            }
        }

        public void LogError(string message)
        {
            if(logger.IsErrorEnabled)
            {
                logger.Error(message);
            }
        }

        public void LogException(string message)
        {
            if(logger.IsFatalEnabled)
            {
                logger.Fatal(message);
            }
        }
    }
}