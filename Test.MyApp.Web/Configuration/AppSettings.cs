using Test.MyApp.Domain.Common;

namespace Test.MyApp.Web.Configuration
{
    public static class AppSettings
    {
        public static void SettingsBinding(this IConfiguration configuration)
        {
            AppConfiguration.ConnectionString = new ConnectionString();
            AppConfiguration.JWTSection = new JwtSection();

            configuration.Bind("ConnectionStrings", AppConfiguration.ConnectionString);
            configuration.Bind("JwtSection", AppConfiguration.JWTSection);
        }
    }
}
