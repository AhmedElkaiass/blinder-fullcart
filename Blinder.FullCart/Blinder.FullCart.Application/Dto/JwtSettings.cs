
namespace Blinder.FullCart.Application.Dto;

public class JwtSettings
{
    public string SigninKey { get; set; }
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public long JwtTokenDurationMunites { get; set; }
    public long RefreshTokenDurationMunites { get; set; }
}
