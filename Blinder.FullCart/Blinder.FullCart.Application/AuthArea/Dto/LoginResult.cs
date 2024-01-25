namespace Blinder.FullCart.Application.AuthArea.Dto;

public record LoginResult
{
    public int StatusId { get; set; }
    public string AccessToken { get; set; }
}
