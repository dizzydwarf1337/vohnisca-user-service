namespace Application.Core.Responses;

public class RpcResponse<T>
{
    public bool IsSuccess { get; set; }
    public T? Data { get; set; }
    public string? Error { get; set; }

    public static RpcResponse<T> Success(T data) =>
        new ()
        {
            IsSuccess = true,
            Data = data
        };

    public static RpcResponse<T> Failure(string? error) =>
        new ()
        {
            IsSuccess = false,
            Error = error
        };
};