using System;
using System.Configuration;

namespace GiveCRM.Web.Infrastructure
{
    public static class ConfigurationSettings
    {
        public static bool IsUserVoiceIntegrationEnabled
        {
            get
            {
                var configSetting = ConfigurationManager.AppSettings["enableUserVoice"];

                if (configSetting == null)
                {
                    return false;
                }

                return configSetting.ToLowerInvariant() == Boolean.TrueString.ToLowerInvariant();
            }
        }
    }
}