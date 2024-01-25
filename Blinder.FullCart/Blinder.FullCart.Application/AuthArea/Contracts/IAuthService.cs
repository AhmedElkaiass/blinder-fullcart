using Blinder.FullCart.Application.AuthArea.Dto;
using Blinder.FullCart.Application.Dto;
namespace Blinder.FullCart.Application.AuthArea.Contracts;
public interface IAuthService
{
    Task<ServiceResponse<LoginResult>> Login(UserLogin login);
}
