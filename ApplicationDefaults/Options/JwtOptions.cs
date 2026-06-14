namespace ApplicationDefaults.Options
{
    // Bound from the "Jwt" configuration section. SigningKey is a secret and must come from
    // user-secrets (dev) or environment variables (prod) — never appsettings.json.
    public class JwtOptions
    {
        public string Issuer { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;
        public string SigningKey { get; set; } = string.Empty;
        public int AccessTokenMinutes { get; set; } = 60;
        public int RefreshTokenDays { get; set; } = 7;
    }
}
