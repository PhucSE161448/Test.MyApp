namespace Test.MyApp.Domain.Common
{
    public class ConnectionString
    {
        public string DefaultConnection { get; set; } = string.Empty;
    }
    public class JwtSection
    {
        public string SecretKey { get; set; } =
            "This is my custom Secret key for authentication MoreThan128bitsSecretKey";
        public bool ValidateIssuerSigningKey { get; set; }
        public string? IssuerSigningKey { get; set; }
        public bool ValidateIssuer { get; set; } = true;
        public string? ValidIssuer { get; set; }
        public bool ValidateAudience { get; set; } = true;
        public string? ValidAudience { get; set; }
        public bool RequireExpirationTime { get; set; }
        public bool ValidateLifetime { get; set; } = true;
        public string AccessTokenExpiryMinutes { get; set; }
        public string RefreshTokenExpiryDays { get; set; }

    }
    public class AppConfiguration
    {
        public static ConnectionString ConnectionString { get; set; }
        public static JwtSection JWTSection { get; set; }
    }
}
