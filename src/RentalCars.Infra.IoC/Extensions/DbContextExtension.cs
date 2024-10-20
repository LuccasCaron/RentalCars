using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RentalCars.Infra.Data.Context;

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

    public static void MigrationInit(this IApplicationBuilder appBuilder)
    {
        using (var serviceScope = appBuilder.ApplicationServices.CreateScope())
        {
            var dbContext = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
            dbContext.Database.Migrate();
        }
    }

}
