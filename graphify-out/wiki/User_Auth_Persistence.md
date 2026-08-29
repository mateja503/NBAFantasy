# User Auth Persistence

> 25 nodes

## Key Concepts

- **Applicationuser** (35 connections) — `NBA.Data/Entities/Applicationuser.cs`
- **.LoginAsync()** (9 connections) — `NBA.Service/Authentication/AuthService.cs`
- **AuthService** (7 connections) — `NBA.Service/Authentication/AuthService.cs`
- **.RegisterAsync()** (7 connections) — `NBA.Service/Authentication/AuthService.cs`
- **LoginResult** (6 connections) — `NBA.Service/Authentication/AuthService.cs`
- **.VerifyPasswordAsync()** (6 connections) — `NBA.Service/Authentication/AuthService.cs`
- **.GetApplicationuserByUsername()** (5 connections) — `NBA.Data/Context/NbaFantasyContextExt.cs`
- **.AddApplicationuser()** (4 connections) — `NBA.Data/Context/NbaFantasyContextExt.cs`
- **.GetApplicationuserById()** (4 connections) — `NBA.Data/Context/NbaFantasyContextExt.cs`
- **.UpdateApplicationuser()** (4 connections) — `NBA.Data/Context/NbaFantasyContextExt.cs`
- **.GetApplicationuser()** (3 connections) — `NBA.Data/Context/NbaFantasyContextExt.cs`
- **Task** (3 connections)
- **Applicationuser.cs** (2 connections) — `NBA.Data/Entities/Applicationuser.cs`
- **ICollection** (1 connections)
- **Email** (1 connections) — `NBA.Data/Entities/Applicationuser.cs`
- **Managerlevel** (1 connections) — `NBA.Data/Entities/Applicationuser.cs`
- **Password** (1 connections) — `NBA.Data/Entities/Applicationuser.cs`
- **Teams** (1 connections) — `NBA.Data/Entities/Applicationuser.cs`
- **Userid** (1 connections) — `NBA.Data/Entities/Applicationuser.cs`
- **Userleagues** (1 connections) — `NBA.Data/Entities/Applicationuser.cs`
- **Username** (1 connections) — `NBA.Data/Entities/Applicationuser.cs`
- **Usertrophies** (1 connections) — `NBA.Data/Entities/Applicationuser.cs`
- **Xp** (1 connections) — `NBA.Data/Entities/Applicationuser.cs`
- **IPasswordHasher** (1 connections)
- **List** (1 connections)

## Relationships

- [EF Core DbContext Model](EF_Core_DbContext_Model.md) (8 shared connections)
- [TeamPlayer & Draft Snapshot Persistence](TeamPlayer_&_Draft_Snapshot_Persistence.md) (5 shared connections)
- [Auth Token Issuance](Auth_Token_Issuance.md) (4 shared connections)
- [Trade & Team Services](Trade_&_Team_Services.md) (4 shared connections)
- [Argon2id Password Hashing](Argon2id_Password_Hashing.md) (3 shared connections)
- [JWT Options & Token Tests](JWT_Options_&_Token_Tests.md) (3 shared connections)
- [Error Codes & Trade Statuses](Error_Codes_&_Trade_Statuses.md) (3 shared connections)
- [Team Entity](Team_Entity.md) (2 shared connections)
- [JWT Token Service](JWT_Token_Service.md) (2 shared connections)
- [Argon2id Hasher Tests](Argon2id_Hasher_Tests.md) (1 shared connections)
- [UserLeague Join Entity](UserLeague_Join_Entity.md) (1 shared connections)
- [Trophy Entities](Trophy_Entities.md) (1 shared connections)

## Source Files

- `NBA.Data/Context/NbaFantasyContextExt.cs`
- `NBA.Data/Entities/Applicationuser.cs`
- `NBA.Service/Authentication/AuthService.cs`

## Audit Trail

- EXTRACTED: 58 (79%)
- INFERRED: 15 (21%)
- AMBIGUOUS: 0 (0%)

---

*Part of the graphify knowledge wiki. See [index](index.md) to navigate.*