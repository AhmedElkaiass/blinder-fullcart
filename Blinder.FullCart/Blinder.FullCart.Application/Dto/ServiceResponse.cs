namespace Blinder.FullCart.Application.Dto;

public record ServiceResponse<ResponseType>
{
    public bool IsSuccess { get; private set; }
    public string Message { get; set; }
    public ResponseType ResponseData { get; set; }
    public Exception Exception { get; set; }
    public static ServiceResponse<ResponseType> Success(ResponseType dto, string message = null) => new ServiceResponse<ResponseType>
    {
        IsSuccess = true,
        Message = message ?? string.Empty,
        ResponseData = dto
    };
    public static ServiceResponse<ResponseType> Error(string message = null, Exception ex = null) => new ServiceResponse<ResponseType>
    {
        IsSuccess = false,
        Message = message ?? string.Empty,
        Exception = ex
    };
}
