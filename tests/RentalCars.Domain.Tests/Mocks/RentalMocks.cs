using RentalCars.Domain.Entities;

namespace RentalCars.Domain.Tests.Mocks;

internal class RentalMocks
{

    #region Properties

    private readonly CarMocks _carMocks = new CarMocks();

    #endregion

    public Rental GetRandomRental()
    {
        var randomCar = _carMocks.GetRandomCar();
        var randomUserId = Guid.NewGuid();
        var rentalStartDate = DateTime.Now;
        var rentalEndDate = DateTime.Now.AddDays(5);

        return Rental.Create(randomCar, randomUserId, rentalStartDate, rentalEndDate);
    }

}
