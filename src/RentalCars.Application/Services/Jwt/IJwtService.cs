
namespace RentalCars.Application.Services.Jwt
{
    public interface IJwtService
    {
        Task<string> GenerateTokenAsync(string email);
    }
}