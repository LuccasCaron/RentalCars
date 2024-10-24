using RentalCars.Domain.Entities;
using RentalCars.Domain.Entities.Exceptions;
using RentalCars.Domain.Tests.Mocks;

namespace RentalCars.Domain.Tests.Entities;

[Trait("Category", "Rental Tests")]
public sealed class RentalTests
{

    #region Properties

    private readonly RentalMocks _rentalMocks = new RentalMocks();
    private readonly CarMocks _carMocks = new CarMocks();

    #endregion

    [Fact]
    public void Create_GivenAllParameters_ThenShouldSetThePropertiesCorrectly()
    {
        // arrange

        Car car = _carMocks.GetRandomCar();
        Guid expectedUserId = Guid.NewGuid();
        DateTime expectedRentalStartDate = DateTime.Now;
        DateTime expectedRentalEndDate = DateTime.Now.AddDays(1);

        // action

        var rentalTest = Rental.Create(car, expectedUserId, expectedRentalStartDate, expectedRentalEndDate);

        // assert

        Assert.Equal(car.Id, rentalTest.CarId);
        Assert.Equal(expectedUserId, rentalTest.UserId);
        Assert.Equal(expectedRentalStartDate, rentalTest.RentalStartDate);
        Assert.Equal(expectedRentalEndDate, rentalTest.RentalEndDate);
        Assert.False(rentalTest.IsCompleted);
        Assert.False(rentalTest.HasPaymentDelay);
        Assert.Equal(0, rentalTest.FineAmount);
    }

    [Fact]
    public void Create_GivenNullCar_ThenShouldThrowArgumentNullException()
    {
        // action
        var exception = Assert.Throws<ArgumentNullException>(() =>
            Rental.Create(null, Guid.NewGuid(), DateTime.Now, DateTime.Now.AddDays(1))
        );

        // assert

        Assert.Equal("car", exception.ParamName);
    }

    [Fact]
    public void Create_GivenEmptyUserId_ThenShouldThrowDomainException()
    {
        // arrange 

        var randomCar = _carMocks.GetRandomCar();

        // action
        var exception = Assert.Throws<DomainException>(() =>
            Rental.Create(randomCar, Guid.Empty, DateTime.Now, DateTime.Now.AddDays(1))
        );

        // assert 
        Assert.Equal("O usuário é obrigatório para o aluguel.", exception.Message);
    }

    [Fact]
    public void Create_GivenStartDateAfterEndDate_ThenShouldThrowDomainException()
    {
        // arrange
        var randomCar = _carMocks.GetRandomCar();

        // action
        var exception = Assert.Throws<DomainException>(() =>
            Rental.Create(randomCar, Guid.NewGuid(), DateTime.Now.AddDays(2), DateTime.Now)
        );

        // assert
        Assert.Equal("A data final do aluguel deve ser maior que a data de início.", exception.Message);
    }

    [Fact]
    public void Create_GivenStartDateEqualsEndDate_ThenShouldThrowDomainException()
    {
        // arrange
        var randomCar = _carMocks.GetRandomCar();
        var now = DateTime.Now;

        // action
        var exception = Assert.Throws<DomainException>(() =>
            Rental.Create(randomCar, Guid.NewGuid(), now, now)
        );

        // assert
        Assert.Equal("A data final do aluguel deve ser maior que a data de início.", exception.Message);
    }

    [Fact]
    public void FinalizeRental_GivenARentalWithIsCompletedTrue_ThenShouldThrowDomainException()
    {
        // arrange
        var rental = _rentalMocks.GetRandomRental();
        rental.FinalizeRental();

        // action
        var exception = Assert.Throws<DomainException>(() => rental.FinalizeRental());

        // assert
        Assert.Equal("Este aluguel já foi finalizado.", exception.Message);
    }

    [Fact]
    public void FinalizeRental_GivenARentalWithIsCompletedFalse_ThenShouldSetIsCompletedTrue()
    {
        // arrange
        var rental = _rentalMocks.GetRandomRental();

        // action
        rental.FinalizeRental();

        // assert
        Assert.True(rental.IsCompleted);
    }

    [Fact]
    public void UpdateRentalEndDate_GivenRentalEndDate_ThenShouldUpdatePropertie()
    {
        // arrange
        var randomRental = _rentalMocks.GetRandomRental();
        var newRentalEndDate = DateTime.Now.AddDays(7);

        // action
        randomRental.UpdateRentalEndDate(newRentalEndDate);

        Assert.Equal(randomRental.RentalEndDate, newRentalEndDate);
    }

    [Fact]
    public void CalculateRentalCost_GivenReturnDateLessThanRentalStartDate_ThenShouldReturnZero()
    {
        // arrange
        var rental = _rentalMocks.GetRandomRental();
        var returnDate = rental.RentalStartDate.AddDays(-1);
        int expectedRentalCost = 0;

        // action
        var returnRentalCost = rental.CalculateRentalCost(returnDate);

        // assert
        Assert.Equal(expectedRentalCost, returnRentalCost);
    }

    [Fact]
    public void CalculateRentalCost_GivenReturnDateBetweenStartDateAndEndDate_ThenShouldReturnProporcionalCost()
    {
        // arrange
        var randomRental = _rentalMocks.GetRandomRental();
        var returnDate = DateTime.Now.AddDays(1);
        var usedDays = (returnDate - randomRental.RentalStartDate).Days;
        var expectedCost = randomRental.Car.DailyRentalPrice * usedDays;

        // action
        var rentalCost = randomRental.CalculateRentalCost(returnDate);

        // assert
        Assert.Equal(expectedCost, rentalCost);
    }

    [Fact]
    public void CalculateRentalCost_GivenReturnDateEqualRentalEndDate_ThenShouldReturnRentalCost()
    {
        // arrange
        var randomRental = _rentalMocks.GetRandomRental();
        var returnDate = DateTime.Now.AddDays(5);
        var usedDays = (randomRental.RentalEndDate - randomRental.RentalStartDate).Days;
        var expectedCost = randomRental.Car.DailyRentalPrice * usedDays;

        // action
        var rentalCost = randomRental.CalculateRentalCost(returnDate);

        // assert
        Assert.Equal(expectedCost, rentalCost);
    }

    [Fact]
    public void CalculateTotalToPay_GivenReturnDateGreaterThanRentalEndDate_ThenShouldReturnRentalCostWithFineAmount()
    {
        // Arrange
        var rental = _rentalMocks.GetRandomRental();

        var dailyFineAmount = 50; // Multa por dia de atraso

        var rentalDays = (rental.RentalEndDate - rental.RentalStartDate).Days;
        var expectedBaseRentalCost = rental.Car.DailyRentalPrice * rentalDays;

        var returnDate = rental.RentalEndDate.AddDays(5);
        var lateDays = (returnDate - rental.RentalEndDate).Days;

        var totalLateFee = lateDays * dailyFineAmount;

        var expectedTotalCost = totalLateFee + expectedBaseRentalCost; // Custo total esperado (aluguel + multa)

        // Action
        var actualTotalCost = rental.CalculateTotalToPay(returnDate);

        // Assert
        Assert.Equal(expectedTotalCost, actualTotalCost);
    }




}
