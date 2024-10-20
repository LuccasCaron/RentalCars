using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RentalCars.Application.Requests.User.Validators;
using RentalCars.Application.Services.Cars;
using RentalCars.Application.Services.Jwt;
using RentalCars.Application.Services.Rentals;
using RentalCars.Application.Services.User;
using RentalCars.Infra.IoC.Extensions;

namespace RentalCars.Infra.IoC;

public static class DependencyInjection
{

    public static IServiceCollection AddInfraestructure(this IServiceCollection services, IConfiguration configuration)
    {

        services.RegisterDbContext(configuration)
                .RegisterIdentity(configuration)
                .RegisterCustomSwaggerGen();

        services.AddValidatorsFromAssemblyContaining<AddUserRequestValidator>();

        services.AddScoped<IUserService, UserService>()
                .AddScoped<IJwtService, JwtService>()
                .AddScoped<ICarService, CarService>()
                .AddScoped<IRentalService, RentalService>();

        return services;
    }

    public static WebApplication RegisterMigrations(this WebApplication application)
    {

        DbContextExtension.MigrationInit(application);

        return application;
    }
}
