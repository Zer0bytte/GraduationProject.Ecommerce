using System.Text.Json.Serialization;

namespace Ecommerce.API.ResponseStructure;

public class ApiResponse<TData>
{
    [JsonConstructor]
    public ApiResponse(string message, TData? data, ApiResponseStatus status)
    {
        Message = message;
        Data = data;
        Status = status.ToString();
    }

    [JsonPropertyName("message")]
    public string Message { get; set; }

    [JsonPropertyName("data")]
    public TData? Data { get; set; }

    [JsonPropertyName("status")]
    public string Status { get; set; }

    public static ApiResponse<TData> Success(TData? data, string message)
    {
        return new ApiResponse<TData>(message, data, ApiResponseStatus.Successful);
    }

    public static ApiResponse<TData> Success(string message)
    {
        return new ApiResponse<TData>(message, default, ApiResponseStatus.Successful);
    }

    public static ApiResponse<TData> Success(TData? data)
    {
        return new ApiResponse<TData>(string.Empty, data, ApiResponseStatus.Successful);
    }

    public static ApiResponse<TData> Failed(string message)
    {
        return new ApiResponse<TData>(message, default, ApiResponseStatus.Failed);
    }
}
