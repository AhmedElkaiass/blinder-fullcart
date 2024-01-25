using Blinder.FullCart.Application.Services;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Blinder.FullCart.Infrastructure.Services;
public class UserSessionService : IUserSessionService
{
    private readonly HttpContext _httpContext;
    const string LanguageHeaderName = "Accept-Language";
    public UserSessionService(IHttpContextAccessor contextAccessor)
    {
        _httpContext = contextAccessor.HttpContext;
    }
    public bool IsLocal => GetHeaderValue(LanguageHeaderName).StartsWith("ar");
    public string Id
    {
        get
        {
            Claim c = GetClaim(ClaimTypes.NameIdentifier);
            if (c != null)
                return c.Value;
            return string.Empty;
        }
    }

    Claim GetClaim(string type) => _httpContext.User.FindFirst(type);
    string GetHeaderValue(string key) => _httpContext?.Request.Headers[key] ?? string.Empty;
}
