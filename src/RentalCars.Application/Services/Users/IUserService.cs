using Microsoft.AspNetCore.Identity;
using RentalCars.Application.Requests.User;
using RentalCars.Application.Responses;

namespace RentalCars.Application.Services.User;

public interface IUserService
{
    Task<ApiResponse<IdentityUser>> AddAsync(AddUserRequest newUser);

    Task<ApiResponse<string>> LoginAsync(LoginUserRequest credentials);
}