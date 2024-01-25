using System.Security.Claims;

namespace Blinder.FullCart.Application.AuthArea.Contracts;

public interface IJWTService
{
    (string accessToken, string refreshToken) GenerateToken(Claim[] claims);
    string GenerateRefreshToken();
    ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
}