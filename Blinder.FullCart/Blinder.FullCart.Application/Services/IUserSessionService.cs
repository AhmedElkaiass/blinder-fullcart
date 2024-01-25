
namespace Blinder.FullCart.Application.Services;

public interface IUserSessionService
{
    public string Id { get; }
    public bool IsLocal { get; }
}
