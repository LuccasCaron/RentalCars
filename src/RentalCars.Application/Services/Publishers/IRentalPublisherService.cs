namespace RentalCars.Application.Services.Publishers;

public interface IRentalPublisherService
{
    Task PublishRentalCreatedAsync(Guid rentalId);
}