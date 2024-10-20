using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace RentalCars.Infra.IoC.Extensions;

internal static class RabbitMQExtension
{

    public static IServiceCollection RegisterRabbitMQ(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMassTransit(busConfigurator =>
        {

            busConfigurator.UsingRabbitMq((ctx, cfg) =>
            {

                cfg.Host(new Uri("amqp://rabbitmq:5672"), host =>
                {
                    host.Username("guest");
                    host.Password("guest");
                });

                cfg.ConfigureEndpoints(ctx);

            });

        });

        return services;
    }

}
