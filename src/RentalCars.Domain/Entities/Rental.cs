using RentalCars.Domain.Entities.Base;

namespace RentalCars.Domain.Entities;

public class Rental : BaseEntity
{

    public Guid CarId { get; private set; }

    public Car Car { get; private set; }

    public Guid UserId { get; private set; }

    public DateTime InitDate { get; private set; }

    public DateTime FinalDate { get; private set; }

    public int DailyRentalPrice { get; private set; }

    public bool HasPaymentDelay { get; private set; }

    public int FineAmount { get; private set; }


}
