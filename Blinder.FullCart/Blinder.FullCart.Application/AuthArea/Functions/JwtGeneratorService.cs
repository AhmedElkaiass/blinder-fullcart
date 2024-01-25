using Blinder.FullCart.Application.AuthArea.Contracts;
using Blinder.FullCart.Domain.Users;
using System.Security.Claims;

namespace Blinder.FullCart.Application.AuthArea.Functions;
public class JwtGeneratorService : IJwtGeneratorService
{
    private readonly IJWTService _jwtService;
    public JwtGeneratorService(IJWTService jwtService) => _jwtService = jwtService;
    public string GetUserTokens(AppUser user)
    {
        var Claims = new Claim[]
       {
            new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
            new Claim(ClaimTypes.Name,user.UserName)
       };
        if (user.UserRoles.Any())
            foreach (var userRole in user.UserRoles)
                Claims.Append(new Claim(ClaimTypes.Role, userRole.RoleId.ToString() ?? string.Empty));
        var Tokens = _jwtService.GenerateToken(Claims);
        return Tokens.accessToken;
    }
}
