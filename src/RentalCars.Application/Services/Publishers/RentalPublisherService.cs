using MassTransit;
using RentalCars.Messaging.Events;

namespace RentalCars.Application.Services.Publishers;

public class RentalPublisherService : IRentalPublisherService
{

    #region Properties

    private readonly IBus _bus;

    #endregion

    #region Constructor

    public RentalPublisherService(IBus bus)
    {
        _bus = bus ?? throw new ArgumentNullException(nameof(bus));
    }

    #endregion


    #region Methods

    public async Task PublishRentalCreatedAsync(Guid rentalId)
    {
        await _bus.Publish(new RentalCreatedEvent { RentalId = rentalId });

        return;
    }

    public async Task PublishRentalFinishAsync(Guid rentalId)
    {
        await _bus.Publish(new RentalFinishEvent { RentalId= rentalId });
    }

    #endregion

}
