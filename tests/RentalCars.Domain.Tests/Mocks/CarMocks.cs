using Bogus;
using RentalCars.Domain.Entities;

namespace RentalCars.Domain.Tests.Mocks;

internal sealed class CarMocks
{

    #region Properties

    private readonly Faker _faker = new("pt_BR");

    #endregion

    public Car GetRandomCar()
    {
        string expectedBrand = _faker.Vehicle.Manufacturer();
        string expectedModel = _faker.Vehicle.Model();
        int expectedYear = _faker.Random.Int(1886, 2024);
        int expectedDailyRentalPrice = _faker.Random.Int(100, 500);

        return Car.Create(expectedBrand, expectedModel, expectedYear, expectedDailyRentalPrice);
    }

}
