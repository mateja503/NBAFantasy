# Applicationuser

> God node · 35 connections · `NBA.Data/Entities/Applicationuser.cs`

**Community:** [User Auth Persistence](User_Auth_Persistence.md)

## Connections by Relation

### calls
- .RegisterAsync() `EXTRACTED`
- .CreateToken_throws_when_signing_key_missing() `INFERRED`
- .CreateToken_issues_a_token_validatable_with_the_same_parameters() `INFERRED`
- .CreateToken_puts_user_id_and_username_in_claims() `INFERRED`

### contains
- Applicationuser.cs `EXTRACTED`

### defines
- Username `EXTRACTED`
- Password `EXTRACTED`
- Email `EXTRACTED`
- Xp `EXTRACTED`
- Managerlevel `EXTRACTED`
- Teams `EXTRACTED`
- Userleagues `EXTRACTED`
- Usertrophies `EXTRACTED`
- Userid `EXTRACTED`

### references
- [NbaFantasyContext](NbaFantasyContext.md) `EXTRACTED`
- [Team](Team.md) `EXTRACTED`
- .OnModelCreating() `EXTRACTED`
- Userleague `EXTRACTED`
- Usertrophie `EXTRACTED`
- .IssueAsync() `EXTRACTED`
- Argon2idPasswordHasher `EXTRACTED`
- Argon2idPasswordHasherTests `EXTRACTED`
- AuthService `EXTRACTED`
- LoginResult `EXTRACTED`
- .CreateToken() `EXTRACTED`
- .VerifyPasswordAsync() `EXTRACTED`
- .GetApplicationuserByUsername() `EXTRACTED`
- .CreateToken() `EXTRACTED`
- .GetApplicationuserById() `EXTRACTED`
- .AddApplicationuser() `EXTRACTED`
- .UpdateApplicationuser() `EXTRACTED`
- .GetApplicationuser() `EXTRACTED`
- .VerifyHashedPassword() `EXTRACTED`
- .HashPassword() `EXTRACTED`

---

*Part of the graphify knowledge wiki. See [index](index.md) to navigate.*