using RentalCars.Domain.Entities.Base;
using RentalCars.Domain.Entities.Exceptions;
using RentalCars.Domain.Entities.Validators;

namespace RentalCars.Domain.Entities;

public class Car : BaseEntity
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

    private void Validate()
    {
        var validator = new CarValidator();

        var result = validator.Validate(this);

        if (!result.IsValid) throw new DomainException(result.Errors[0].ErrorMessage);
    }

    #endregion
}
