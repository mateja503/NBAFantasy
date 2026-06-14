using NBA.Api.Authentication;
using Xunit;

namespace NBA.Tests
{
    public class RefreshTokenGeneratorTests
    {
        [Fact]
        public void Generate_produces_unique_url_safe_tokens()
        {
            var tokens = Enumerable.Range(0, 1000)
                .Select(_ => RefreshTokenGenerator.Generate())
                .ToList();

            // No collisions across a thousand draws.
            Assert.Equal(tokens.Count, tokens.Distinct().Count());

            // URL-safe: no '+', '/' or '=' padding that would break in query strings / headers.
            Assert.All(tokens, t =>
            {
                Assert.DoesNotContain('+', t);
                Assert.DoesNotContain('/', t);
                Assert.DoesNotContain('=', t);
                Assert.False(string.IsNullOrWhiteSpace(t));
            });
        }

        [Fact]
        public void Hash_is_deterministic_and_hides_the_token()
        {
            var token = RefreshTokenGenerator.Generate();

            var first = RefreshTokenGenerator.Hash(token);
            var second = RefreshTokenGenerator.Hash(token);

            Assert.Equal(first, second);                 // same input -> same key
            Assert.NotEqual(token, first);               // stored value is not the token
            Assert.Equal(64, first.Length);              // SHA-256 as hex = 64 chars
            Assert.NotEqual(first, RefreshTokenGenerator.Hash(RefreshTokenGenerator.Generate()));
        }
    }
}
