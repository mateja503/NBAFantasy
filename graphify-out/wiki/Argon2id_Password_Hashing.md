# Argon2id Password Hashing

> 11 nodes

## Key Concepts

- **Argon2idPasswordHasher** (8 connections) — `NBA.Service/Authentication/Argon2idPasswordHasher.cs`
- **Argon2Options** (6 connections) — `ApplicationDefaults/Options/Argon2Options.cs`
- **.VerifyHashedPassword()** (3 connections) — `NBA.Service/Authentication/Argon2idPasswordHasher.cs`
- **.HashPassword()** (2 connections) — `NBA.Service/Authentication/Argon2idPasswordHasher.cs`
- **Argon2Options.cs** (2 connections) — `ApplicationDefaults/Options/Argon2Options.cs`
- **DegreeOfParallelism** (1 connections) — `ApplicationDefaults/Options/Argon2Options.cs`
- **Iterations** (1 connections) — `ApplicationDefaults/Options/Argon2Options.cs`
- **MemoryKib** (1 connections) — `ApplicationDefaults/Options/Argon2Options.cs`
- **IPasswordHasher** (1 connections)
- **IOptions** (1 connections)
- **PasswordVerificationResult** (1 connections)

## Relationships

- [User Auth Persistence](User_Auth_Persistence.md) (3 shared connections)
- [Argon2id Hasher Tests](Argon2id_Hasher_Tests.md) (2 shared connections)
- [Team Endpoints & Auth Claims](Team_Endpoints_&_Auth_Claims.md) (1 shared connections)
- [Draft Endpoints Integration Tests](Draft_Endpoints_Integration_Tests.md) (1 shared connections)

## Source Files

- `ApplicationDefaults/Options/Argon2Options.cs`
- `NBA.Service/Authentication/Argon2idPasswordHasher.cs`

## Audit Trail

- EXTRACTED: 16 (94%)
- INFERRED: 1 (6%)
- AMBIGUOUS: 0 (0%)

---

*Part of the graphify knowledge wiki. See [index](index.md) to navigate.*