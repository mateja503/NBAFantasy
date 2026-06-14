using System.Security.Cryptography;
using System.Text;
using ApplicationDefaults.Options;
using Isopoh.Cryptography.Argon2;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using NBA.Data.Entities;

namespace NBA.Service.Authentication
{
    // Argon2id password hasher. Argon2id is memory-hard, which resists the GPU/ASIC cracking that
    // PBKDF2 is weaker against, and was the Password Hashing Competition winner. The produced string
    // is PHC-encoded ($argon2id$v=19$m=...,t=...,p=...$salt$hash), so it carries its own parameters
    // and salt — verification needs no external state and work factors can be raised over time.
    public class Argon2idPasswordHasher(IOptions<Argon2Options> options) : IPasswordHasher<Applicationuser>
    {
        private readonly Argon2Options _options = options.Value;

        public string HashPassword(Applicationuser user, string password)
        {
            var config = new Argon2Config
            {
                Type = Argon2Type.HybridAddressing, // Argon2id
                Version = Argon2Version.Nineteen,
                TimeCost = _options.Iterations,
                MemoryCost = _options.MemoryKib,
                Lanes = _options.DegreeOfParallelism,
                Threads = _options.DegreeOfParallelism,
                Password = Encoding.UTF8.GetBytes(password),
                Salt = RandomNumberGenerator.GetBytes(16), // unique per password
                HashLength = 32,
            };

            return Argon2.Hash(config);
        }

        public PasswordVerificationResult VerifyHashedPassword(Applicationuser user, string hashedPassword, string providedPassword)
        {
            try
            {
                // Constant-time comparison handled inside Verify.
                return Argon2.Verify(hashedPassword, providedPassword)
                    ? PasswordVerificationResult.Success
                    : PasswordVerificationResult.Failed;
            }
            catch
            {
                // Stored value isn't a valid Argon2 hash (e.g. legacy plaintext) — let the caller's
                // migrate-on-login path handle it.
                return PasswordVerificationResult.Failed;
            }
        }
    }
}
