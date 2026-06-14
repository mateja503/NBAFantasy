using ApplicationDefaults.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using NBA.Data.Entities;
using NBA.Service.Authentication;
using Xunit;

namespace NBA.Tests
{
    public class Argon2idPasswordHasherTests
    {
        // Small work factors keep the test fast; production values come from configuration.
        private static Argon2idPasswordHasher CreateSut() =>
            new(Microsoft.Extensions.Options.Options.Create(new Argon2Options
            {
                MemoryKib = 1024,
                Iterations = 1,
                DegreeOfParallelism = 1,
            }));

        private static readonly Applicationuser User = new() { Userid = 1, Username = "u" };

        [Fact]
        public void Hash_then_verify_succeeds_for_correct_password()
        {
            var sut = CreateSut();
            var hash = sut.HashPassword(User, "s3cret!");

            Assert.Equal(PasswordVerificationResult.Success,
                sut.VerifyHashedPassword(User, hash, "s3cret!"));
        }

        [Fact]
        public void Verify_fails_for_wrong_password()
        {
            var sut = CreateSut();
            var hash = sut.HashPassword(User, "s3cret!");

            Assert.Equal(PasswordVerificationResult.Failed,
                sut.VerifyHashedPassword(User, hash, "wrong"));
        }

        [Fact]
        public void Hash_is_argon2id_and_salted_so_two_hashes_differ()
        {
            var sut = CreateSut();

            var a = sut.HashPassword(User, "samePassword");
            var b = sut.HashPassword(User, "samePassword");

            Assert.StartsWith("$argon2id$", a);
            Assert.NotEqual(a, b); // random per-password salt
        }

        [Fact]
        public void Verify_returns_failed_for_legacy_plaintext_value()
        {
            var sut = CreateSut();

            // A non-Argon2 stored value must not throw — it should report Failed so the caller's
            // migrate-on-login path can take over.
            Assert.Equal(PasswordVerificationResult.Failed,
                sut.VerifyHashedPassword(User, "plaintextPassword", "plaintextPassword"));
        }
    }
}
