using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Test.MyApp.Domain.Common;

namespace Test.MyApp.Web.Configuration
{
    public static class Authentication
    {
        public static IServiceCollection AddJwtValidation(this IServiceCollection services)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = AppConfiguration.JWTSection.ValidIssuer,
                    ValidAudience = AppConfiguration.JWTSection.ValidAudience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AppConfiguration.JWTSection.SecretKey)),
                    CryptoProviderFactory = new CryptoProviderFactory { CacheSignatureProviders = false }
                };
            });
            return services;
        }
    }
}
