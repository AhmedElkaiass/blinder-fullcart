using Blinder.FullCart.Application.AuthArea.Contracts;
using Blinder.FullCart.Application.AuthArea.Dto;
using Blinder.FullCart.Application.Dto;
using Blinder.FullCart.Application.Services;
using Blinder.FullCart.Domain.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;

namespace Blinder.FullCart.Application.AuthArea.Functions;

public class AuthService : IAuthService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly IStringLocalizer<AuthService> _localizer;
    private readonly JwtSettings _jwtSettings;
    private readonly IUserSessionService _userSessionService;
    private readonly IJwtGeneratorService _jwtGeneratorService;

    public AuthService(
        UserManager<AppUser> userManager
        , SignInManager<AppUser> signInManager
        , IStringLocalizer<AuthService> localizer
        , IOptions<JwtSettings> options
        , IUserSessionService userSessionService
        , IJwtGeneratorService jwtGeneratorService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _localizer = localizer;
        _jwtSettings = options.Value;
        _userSessionService = userSessionService;
        _jwtGeneratorService = jwtGeneratorService;
    }
    public async Task<ServiceResponse<LoginResult>> Login(UserLogin login)
    {
        throw new NotImplementedException();
    }

    async Task<AppUser> GetUser(string userName)
    {
        return await _userManager.Users
            .Include(x => x.UserRoles)
            .ThenInclude(x => x.Role)
            .Where(x => x.Email.Equals(userName) || x.UserName.Equals(userName) || x.PhoneNumber.Equals(userName))
            .SingleOrDefaultAsync();
    }
}
