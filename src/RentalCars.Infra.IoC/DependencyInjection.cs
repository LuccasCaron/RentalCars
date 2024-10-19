using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RentalCars.Application.Requests.User.Validators;
using RentalCars.Application.Services.Cars;
using RentalCars.Application.Services.Jwt;
using RentalCars.Application.Services.User;
using RentalCars.Infra.IoC.Extensions;

namespace RentalCars.Infra.IoC;

public static class DependencyInjection
{

    public static IServiceCollection AddInfraestructure(this IServiceCollection services, IConfiguration configuration)
    {

        services.RegisterDbContext(configuration)
                .RegisterIdentity(configuration);

        services.AddValidatorsFromAssemblyContaining<AddUserRequestValidator>();

        services.AddScoped<IUserService, UserService>()
                .AddScoped<IJwtService, JwtService>()
                .AddScoped<ICarService, CarService>();


        return services;
    }

}
