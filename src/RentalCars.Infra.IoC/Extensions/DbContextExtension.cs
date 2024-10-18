using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RentalCars.Infra.Data.Context;
using System;

namespace RentalCars.Infra.IoC.Extensions;

internal static class DbContextExtension
{

    internal static IServiceCollection RegisterDbContext(this IServiceCollection services, IConfiguration configuration)
    {

        string connectionStr = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<ApplicationDbContext>(opts =>
        {
            opts.UseNpgsql(connectionStr,
                builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName));
        });

        return services;

    }

}
