namespace Application.Core.Responses;

public class RpcResponse<T>
{
    public bool IsSuccess { get; set; }
    public T? Data { get; set; }
    public string? Error { get; set; }
    public int StatusCode { get; set; } = 500;

    public static RpcResponse<T> Success(T data) =>
        new()
        {
            IsSuccess = true,
            Data = data,
            StatusCode = 200,
        };

    public static RpcResponse<T> Failure(string? error, int statusCode = 500) =>
        new()
        {
            IsSuccess = false,
            Error = error,
            StatusCode = statusCode,
        };
}