# Argon2id Hasher Tests

> 7 nodes

## Key Concepts

- **Argon2idPasswordHasherTests** (7 connections) — `NBA.Tests/Argon2idPasswordHasherTests.cs`
- **.CreateSut()** (7 connections) — `NBA.Tests/Argon2idPasswordHasherTests.cs`
- **Fact** (4 connections)
- **.Hash_is_argon2id_and_salted_so_two_hashes_differ()** (3 connections) — `NBA.Tests/Argon2idPasswordHasherTests.cs`
- **.Hash_then_verify_succeeds_for_correct_password()** (3 connections) — `NBA.Tests/Argon2idPasswordHasherTests.cs`
- **.Verify_fails_for_wrong_password()** (3 connections) — `NBA.Tests/Argon2idPasswordHasherTests.cs`
- **.Verify_returns_failed_for_legacy_plaintext_value()** (3 connections) — `NBA.Tests/Argon2idPasswordHasherTests.cs`

## Relationships

- [Argon2id Password Hashing](Argon2id_Password_Hashing.md) (2 shared connections)
- [User Auth Persistence](User_Auth_Persistence.md) (1 shared connections)
- [Team Endpoints & Auth Claims](Team_Endpoints_&_Auth_Claims.md) (1 shared connections)

## Source Files

- `NBA.Tests/Argon2idPasswordHasherTests.cs`

## Audit Trail

- EXTRACTED: 16 (94%)
- INFERRED: 1 (6%)
- AMBIGUOUS: 0 (0%)

---

*Part of the graphify knowledge wiki. See [index](index.md) to navigate.*