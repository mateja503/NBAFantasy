using System.Text;
using ApplicationDefaults.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using NBA.Api.Authentication;
using NBA.Data.Entities;
using Xunit;

namespace NBA.Tests
{
    public class JwtTokenServiceTests
    {
        private static readonly JwtOptions Options = new()
        {
            Issuer = "nba-fantasy-api",
            Audience = "nba-fantasy-client",
            // Test-only key; must be >= 256 bits for HS256.
            SigningKey = "unit-test-signing-key-that-is-long-enough-1234567890",
            AccessTokenMinutes = 30,
        };

        private static JwtTokenService CreateSut() =>
            new(Microsoft.Extensions.Options.Options.Create(Options));

        [Fact]
        public async Task CreateToken_issues_a_token_validatable_with_the_same_parameters()
        {
            var user = new Applicationuser { Userid = 42, Username = "coachK" };

            var result = CreateSut().CreateToken(user);

            Assert.False(string.IsNullOrWhiteSpace(result.AccessToken));
            Assert.True(result.ExpiresAtUtc > DateTime.UtcNow);

            var handler = new JsonWebTokenHandler();
            var validation = handler.ValidateTokenAsync(result.AccessToken, new TokenValidationParameters
            {
                ValidIssuer = Options.Issuer,
                ValidAudience = Options.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Options.SigningKey)),
            }).GetAwaiter().GetResult();

            Assert.True(validation.IsValid);
        }

        [Fact]
        public void CreateToken_puts_user_id_and_username_in_claims()
        {
            var user = new Applicationuser { Userid = 42, Username = "coachK" };

            var token = CreateSut().CreateToken(user).AccessToken;

            var jwt = new JsonWebTokenHandler().ReadJsonWebToken(token);
            Assert.Equal("42", jwt.GetClaim(JwtRegisteredClaimNames.Sub).Value);
            Assert.Equal("coachK", jwt.GetClaim(JwtRegisteredClaimNames.UniqueName).Value);
        }

        [Fact]
        public void CreateToken_throws_when_signing_key_missing()
        {
            var sut = new JwtTokenService(Microsoft.Extensions.Options.Options.Create(new JwtOptions
            {
                Issuer = "i",
                Audience = "a",
                SigningKey = "",
            }));

            Assert.Throws<InvalidOperationException>(() => sut.CreateToken(new Applicationuser { Userid = 1 }));
        }
    }
}
