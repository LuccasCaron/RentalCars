using RentalCars.Domain.Entities.Base;
using RentalCars.Domain.Entities.Exceptions;
using RentalCars.Domain.Entities.Validators;

namespace RentalCars.Domain.Entities;

public class Rental : BaseEntity
{

    #region Properties

    public Guid CarId { get; private set; }

    public Car Car { get; private set; }

    public Guid UserId { get; private set; }

    public DateTime RentalStartDate { get; private set; }

    public DateTime RentalEndDate { get; private set; }

    public int AppliedDailyPrice { get; private set; }

    public bool IsCompleted { get; private set; }

    public bool HasPaymentDelay { get; private set; }

    public int FineAmount { get; private set; }

    #endregion

    #region Constructor

    protected Rental() { }

    private Rental(Car car, Guid userId, DateTime rentalStarDate, DateTime rentalEndDate)
    {
        Car = car ?? throw new ArgumentNullException(nameof(car));
        CarId = car.Id;
        UserId = userId;
        RentalStartDate = rentalStarDate;
        RentalEndDate = rentalEndDate;
        AppliedDailyPrice = car.DailyRentalPrice;
        IsCompleted = false;
        HasPaymentDelay = false;
        FineAmount = 0;

        Validate();
    }

    #endregion

    #region Factory

    public static Rental Create(Car car, Guid userId, DateTime rentalStarDate, DateTime rentalEndDate)
    {
        Rental rental = new(car, userId, rentalStarDate, rentalEndDate);

        rental.Validate();

        return rental;
    }

    #endregion

    #region Methods

    public int CalculateTotalToPay(DateTime returnDate)
    {
        int totalToPay = CalculateRentalCost(returnDate);

        if (returnDate > RentalEndDate)
        {
            int lateFee = CalculateLateFee(returnDate);
            totalToPay += lateFee;
        }

        return totalToPay;
    }

    public int CalculateRentalCost(DateTime returnDate)
    {
        if (returnDate < RentalStartDate)
        {
            return 0;
        }

        int daysUsed;

        if (returnDate > RentalStartDate && returnDate < RentalEndDate)
        {
            daysUsed = (returnDate - RentalStartDate).Days;

            return daysUsed * AppliedDailyPrice;
        }

        daysUsed = (RentalEndDate - RentalStartDate).Days;

        return daysUsed * AppliedDailyPrice;
    }

    public void UpdateRentalEndDate(DateTime returnDate)
    {
        if (DateTime.Now > RentalEndDate)
        {
            throw new DomainException("Não é possível atualizar a data do aluguel depois da data final atual.");
        }

        RentalEndDate = returnDate;
    }

    public void FinalizeRental()
    {
        if (IsCompleted)
        {
            throw new DomainException("Este aluguel já foi finalizado.");
        }

        IsCompleted = true; 
    }

    private int CalculateLateFee(DateTime returnDate)
    {
        HasPaymentDelay = true;

        int daysLate = (returnDate - RentalEndDate).Days;
        int dailyFine = 50;

        FineAmount = daysLate * dailyFine;

        return FineAmount;
    }

    private void Validate()
    {
        var validator = new RentalValidator();

        var result = validator.Validate(this);

        if (!result.IsValid) throw new DomainException(result.Errors[0].ErrorMessage);
    }

    #endregion

}
