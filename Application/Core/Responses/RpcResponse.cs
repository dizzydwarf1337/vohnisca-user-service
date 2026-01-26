namespace Application.Core.Responses;

public class RpcResponse<T>
{
    public bool IsSuccess;
    public T? Data;
    public string? Error;

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