using Bogus;
using RentalCars.Domain.Entities;
using RentalCars.Domain.Entities.Exceptions;
using RentalCars.Domain.Tests.Mocks;

namespace RentalCars.Domain.Tests.Entities;

[Trait("Category", "Car Tests")]
public sealed class CarTests
{

    #region Properties
    
    private readonly Faker _faker = new("pt_BR");
    private readonly CarMocks _carMocks = new CarMocks();

    #endregion

    #region Tests

    [Fact]
    public void Create_GivenAllParameters_ThenShouldSetThePropertiesCorrectly()
    {
        // arrange

        string expectedBrand = _faker.Vehicle.Manufacturer();
        string expectedModel = _faker.Vehicle.Model();
        int expectedYear = _faker.Random.Int(1886, 2024);
        int expectedDailyRentalPrice = _faker.Random.Int(100, 500);

        // action

        var carTest = Car.Create(expectedBrand, expectedModel, expectedYear, expectedDailyRentalPrice);

        // assert

        Assert.Equal(expectedBrand, carTest.Brand);
        Assert.Equal(expectedModel, carTest.Model);
        Assert.Equal(expectedYear, carTest.Year);
        Assert.Equal(expectedDailyRentalPrice, carTest.DailyRentalPrice);
    }

    [Fact]
    public void Create_GivenEmptyBrand_ThenShouldThrowDomainException()
    {
        // arrange

        string expectedBrand = "";
        string expectedModel = _faker.Vehicle.Model();
        int expectedYear = _faker.Random.Int(1886, 2024);
        int expectedDailyRentalPrice = _faker.Random.Int(100, 500);

        // action

        var exception = Assert.Throws<DomainException>(
            () => Car.Create(expectedBrand, expectedModel, expectedYear, expectedDailyRentalPrice));

        // assert

        Assert.Equal("A marca é obrigatória.", exception.Message);
    }

    [Fact]
    public void Create_GivenEmptyModel_ThenShouldThrowDomainException()
    {
        // arrange

        string expectedBrand = _faker.Vehicle.Manufacturer();
        string expectedModel = "";
        int expectedYear = _faker.Random.Int(1886, 2024);
        int expectedDailyRentalPrice = _faker.Random.Int(100, 500);

        // action

        var exception = Assert.Throws<DomainException>(
            () => Car.Create(expectedBrand, expectedModel, expectedYear, expectedDailyRentalPrice));

        // assert

        Assert.Equal("O modelo do carro é obrigatório.", exception.Message);
    }

    [Fact]
    public void Create_GivenYearLessThan1866_ThenShouldThrowDomainException()
    {
        // arrange

        string expectedBrand = _faker.Vehicle.Manufacturer();
        string expectedModel = _faker.Vehicle.Model();
        int expectedYear = 1885;
        int expectedDailyRentalPrice = _faker.Random.Int(100, 500);

        // action

        var exception = Assert.Throws<DomainException>(
            () => Car.Create(expectedBrand, expectedModel, expectedYear, expectedDailyRentalPrice));

        // assert

        Assert.Equal("O ano deve estar entre 1886 e o ano atual.", exception.Message);
    }

    [Fact]
    public void SetAvailibilityFalse_GivenACarWithAvailibilityTrue_ThenReturnPropertyUpdated()
    {
        // arrange

        Car randomCarWithAvailibilityTrue = _carMocks.GetRandomCar();

        //action

        randomCarWithAvailibilityTrue.SetAvailibilityFalse();

        // assert

        Assert.False(randomCarWithAvailibilityTrue.Availability);

    }

    [Fact]
    public void SetAvailibilityFalse_GivenACarWithAvailibilityFalse_ThenReturnDomainException()
    {
        // arrange, setando carro para falso e tentando novamente alterar a prop

        Car randomCarWithAvailibilityTrue = _carMocks.GetRandomCar();
        randomCarWithAvailibilityTrue.SetAvailibilityFalse();

        //action

        var exception = Assert.Throws<DomainException>(() => randomCarWithAvailibilityTrue.SetAvailibilityFalse());

        // assert

        Assert.Equal("Este carro não está disponível.", exception.Message);

    }

    [Fact]
    public void UpdateDailyRentalPrice_GivenDailyRentalPriceLessThanZero_ThenShouldThrowDomainException()
    {
        // arrange

        Car randomCar = _carMocks.GetRandomCar();

        // action

        var exception = Assert.Throws<DomainException>(() => randomCar.UpdateDailyRentalPrice(-1));

        // assert

        Assert.Equal("O Preço diário não pode ser menor que 0.", exception.Message);
    }

    [Fact]
    public void UpdateDailyRentalPrice_GivenDailyRentalPriceGreaterThanZero_ThenShouldReturnUpdatedProperty()
    {
        // arrange

        Car randomCar = _carMocks.GetRandomCar();

        // action

        randomCar.UpdateDailyRentalPrice(300);

        // assert

        Assert.Equal(300, randomCar.DailyRentalPrice);
    }

    #endregion

}
