using RentalCars.Domain.Entities.Base;
using RentalCars.Domain.Entities.Exceptions;
using RentalCars.Domain.Entities.Validators;

namespace RentalCars.Domain.Entities;

public class Rental : BaseEntity
{
    public Guid CarId { get; private set; }

    public Car Car { get; private set; }

    public Guid UserId { get; private set; }

    public DateTime InitDate { get; private set; }

    public DateTime FinalDate { get; private set; }

    public int AppliedDailyPrice { get; private set; }

    public bool HasPaymentDelay { get; private set; }

    public int FineAmount { get; private set; }

    #region Constructor

    protected Rental() { }

    private Rental(Car car, Guid userId, DateTime initDate, DateTime finalDate)
    {
        Car = car ?? throw new ArgumentNullException(nameof(car));
        CarId = car.Id;
        UserId = userId;
        InitDate = initDate;
        FinalDate = finalDate;
        AppliedDailyPrice = car.DailyRentalPrice;
        HasPaymentDelay = false; 
        FineAmount = 0; 

        Validate();
    }

    #endregion

    #region Factory

    public static Rental Create(Car car, Guid userId, DateTime finalDate)
    {
        Rental rental = new(car, userId, DateTime.Now, finalDate);

        rental.Validate();

        return rental;
    }

    #endregion

    #region Methods

    public void ApplyLateFee(int fineAmount)
    {
        FineAmount = fineAmount;
        HasPaymentDelay = true;
    }

    private void Validate()
    {
        var validator = new RentalValidator();

        var result = validator.Validate(this);

        if (!result.IsValid) throw new DomainException(result.Errors[0].ErrorMessage);
    }

    #endregion
}
