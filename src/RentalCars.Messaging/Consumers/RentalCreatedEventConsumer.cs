using MassTransit;
using RentalCars.Infra.Data.Context;
using RentalCars.Messaging.Events;

namespace RentalCars.Messaging.Consumers;

public class RentalCreatedEventConsumer : IConsumer<RentalCreatedEvent>
{

    #region Properties

    private readonly ApplicationDbContext _context;

    #endregion

    #region Constructor

    public RentalCreatedEventConsumer(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    #endregion

    public async Task Consume(ConsumeContext<RentalCreatedEvent> context)
    {
        var rentalId = context.Message.RentalId;

        var rental = await _context.Rentals.FindAsync(rentalId)
                                           .ConfigureAwait(false);

        if (rental is not null)
        {
            // Exemplo: enviar um email de confirmação de aluguel
            // Implementar lógica de envio de email aqui
        }
    }
}
