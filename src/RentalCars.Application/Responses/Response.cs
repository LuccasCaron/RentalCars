using System.Text.Json.Serialization;

namespace RentalCars.Application.Responses;

public record Response<TData>
{
    private readonly int _code;
    public string? Message { get; init; } = string.Empty;
    public TData? Data { get; init; }

    [JsonConstructor]
    public Response() => _code = 200;

    public Response(TData? data, int code = 200, string? message = null)
    {
        Message = message;
        Data = data;
        _code = code;
    }

    [JsonIgnore]
    public bool IsSuccess => _code is >= 200 and <= 299;
    

}
