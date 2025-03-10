namespace ReservationSystem.Shared.Results;

public class ApiResult<T>
{
    public T? Data { get; set; }
    
    public bool Success { get; set; }
    
    public string? Message { get; set; }
    
    public ApiResult(T? data, bool success = true, string? message = default)
    {
        Data = data;
        Success = success;
        Message = message;
    }
}