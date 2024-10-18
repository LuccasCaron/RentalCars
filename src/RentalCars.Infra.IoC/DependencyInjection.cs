using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RentalCars.Infra.IoC.Extensions;

namespace RentalCars.Infra.IoC;

public static class DependencyInjection
{

    public static IServiceCollection AddInfraestructure(this IServiceCollection services, IConfiguration configuration)
    {

        services.RegisterDbContext(configuration);


        return services;
    }

}
