﻿using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RentalCars.Application.Requests.User.Validators;
using RentalCars.Application.Services.Cars;
using RentalCars.Application.Services.Jwt;
using RentalCars.Application.Services.Publishers;
using RentalCars.Application.Services.Rentals;
using RentalCars.Application.Services.User;
using RentalCars.Infra.Email.Services;
using RentalCars.Infra.IoC.Extensions;

namespace RentalCars.Infra.IoC;

public static class DependencyInjection
{

    public static IServiceCollection AddInfraestructure(this IServiceCollection services, IConfiguration configuration)
    {

        services.RegisterDbContext(configuration)
                .RegisterIdentity(configuration)
                .RegisterRabbitMQ(configuration)
                .RegisterCustomSwaggerGen();

        services.AddValidatorsFromAssemblyContaining<AddUserRequestValidator>();

        services.AddScoped<IUserService, UserService>()
                .AddScoped<IJwtService, JwtService>()
                .AddScoped<ICarService, CarService>()
                .AddScoped<IRentalService, RentalService>()
                .AddScoped<IRentalPublisherService, RentalPublisherService>()
                .AddScoped<IEmailService, EmailService>();

        return services;
    }

    public static WebApplication RegisterMigrations(this WebApplication application)
    {

        DbContextExtension.MigrationInit(application);

        return application;
    }
}
