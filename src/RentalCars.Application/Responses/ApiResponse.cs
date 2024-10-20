using System.Text.Json.Serialization;

namespace RentalCars.Application.Responses;

public record ApiResponse<TData>
{
    private readonly int _code;
    public string? Message { get; init; } = string.Empty;
    public TData? Data { get; init; }

    [JsonConstructor]
    public ApiResponse() => _code = 200;

    public ApiResponse(TData? data, int code = 200, string? message = null)
    {
        Message = message;
        Data = data;
        _code = code;
    }

    [JsonIgnore]
    public bool IsSuccess => _code is >= 200 and <= 299;
    

}
