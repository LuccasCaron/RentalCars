using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RentalCars.Messaging.Consumers;

namespace RentalCars.Infra.IoC.Extensions;

internal static class RabbitMQExtension
{

    public static IServiceCollection RegisterRabbitMQ(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMassTransit(busConfigurator =>
        {

            busConfigurator.AddConsumer<RentalCreatedEventConsumer>();

            busConfigurator.UsingRabbitMq((ctx, cfg) =>
            {

                cfg.Host(new Uri(configuration.GetValue<string>("RabbitMQ:endpoint")), host =>
                {
                    host.Username(configuration.GetValue<string>("RabbitMQ:username"));
                    host.Password(configuration.GetValue<string>("RabbitMQ:password"));
                });

                cfg.ConfigureEndpoints(ctx);

            });

        });

        return services;
    }

}
