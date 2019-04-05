using FashFuns.Common.Helpers;
using Microsoft.Extensions.Configuration;
namespace FashFuns.Common
{
    public class ConfigurationManager
    {
        private static string _localDatabaseConnectionString;

        private static AppSettings _appSettings;

        public static void SetAppSettingsProperties(IConfiguration configuration)
        {
            _localDatabaseConnectionString = configuration.GetConnectionString(ConnectionStringNames.Local);

            AppSettings appSettings = new AppSettings();
            appSettings.TokenSecret  = configuration.GetSection("AppSettings")["TokenSecret"];
            appSettings.TokenExpiryDays = int.Parse(configuration.GetSection("AppSettings")["TokenExpiryDays"]);
            appSettings.PasswordWeakErrorMessage = configuration.GetSection("AppSettings")["PasswordWeakErrorMessage"];
            appSettings.PasswordStrongRegex = configuration.GetSection("AppSettings")["PasswordStrongRegex"];
            appSettings.PasswordExpiryDays = int.Parse(configuration.GetSection("AppSettings")["PasswordExpiryDays"]);

            _appSettings = appSettings;
        }

        public static string LocalDatabaseConnectionString => _localDatabaseConnectionString;

        public static AppSettings AppSettings => _appSettings;
    }
}
