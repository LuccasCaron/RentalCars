using Microsoft.AspNetCore.Identity;
using RentalCars.Application.Requests.User;
using RentalCars.Application.Responses;

namespace RentalCars.Application.Services.User;

public interface IUserService
{
    Task<Response<IdentityUser>> AddAsync(AddUserRequest newUser);

    Task<Response<string>> LoginAsync(LoginUserRequest credentials);
}