using RentalCars.Domain.Entities.Base;
using RentalCars.Domain.Entities.Exceptions;
using RentalCars.Domain.Entities.Validators;

namespace RentalCars.Domain.Entities;

public sealed class Car : BaseEntity
{

    #region Properties

    public string Brand { get; private set; } = string.Empty;

    public string Model { get; private set; } = string.Empty;

    public int Year { get; private set; }

    public bool Availability { get; private set; }

    public int DailyRentalPrice { get; private set; }

    #endregion

    #region Constructor

    private Car(string brand, string model, int year, bool availability, int dailyRentalPrice)
    {
        Brand = brand;
        Model = model;
        Year = year;
        Availability = availability;
        DailyRentalPrice = dailyRentalPrice;

        Validate();
    }

    #endregion

    #region Factory

    public static Car Create(string brand, string model, int year, int dailyRentalPrice)
    {
        var car = new Car(brand, model, year, true, dailyRentalPrice);
        car.Validate();

        return car;
    }

    #endregion

    #region Methods

    public void SetAvailibilityFalse()
    {   
        if (!Availability)
        {
            throw new DomainException("Este carro não está disponível.");
        }

        Availability = false;
    }

    public void SetAvailibilityTrue()
    {
        Availability = true;
    }

    public void UpdateDailyRentalPrice(int newDailyRentalPrice)
    {
        if(newDailyRentalPrice < 0)
        {
            throw new DomainException("O Preço diário não pode ser menor que 0.");
        }

        DailyRentalPrice = newDailyRentalPrice;
    }

    private void Validate()
    {
        var validator = new CarValidator();

        var result = validator.Validate(this);

        if (!result.IsValid) throw new DomainException(result.Errors[0].ErrorMessage);
    }

    #endregion

}
