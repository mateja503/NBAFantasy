---
name: first-time-setup
description: Set up the secrets NBAFantasy needs to run for the first time - the balldontlie API key, the JWT signing key and the Postgres password, stored as Aspire parameters in dotnet user-secrets on the NBAFantasy AppHost project. Use when the app fails to start on a fresh clone or when a secret needs rotating.
---

# First-time secret setup

The AppHost reads secrets as Aspire parameters from user-secrets (on the `NBAFantasy` project). Run once:
```
cd NBAFantasy
dotnet user-secrets set "Parameters:postgress-password" "<value>"
dotnet user-secrets set "Parameters:balldontlie-apikey" "<value>"
dotnet user-secrets set "Parameters:jwt-signing-key" "<value>"
```
