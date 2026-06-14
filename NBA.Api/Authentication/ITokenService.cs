using NBA.Data.Entities;

namespace NBA.Api.Authentication
{
    public record AuthToken(string AccessToken, DateTime ExpiresAtUtc);

    public interface ITokenService
    {
        AuthToken CreateToken(Applicationuser user);
    }
}
