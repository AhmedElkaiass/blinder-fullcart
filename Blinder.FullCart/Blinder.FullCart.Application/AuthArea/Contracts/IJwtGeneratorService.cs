using Blinder.FullCart.Domain.Users;

namespace Blinder.FullCart.Application.AuthArea.Contracts;
public interface IJwtGeneratorService
{
    string GetUserTokens(AppUser user);
}
