using RentalCars.Domain.Entities;

namespace RentalCars.Application.Responses.Rentals;

public class FinalizedRentalResponse
{
    public Rental Rental { get; set; }
    public int TotalToPay { get; set; }

    public FinalizedRentalResponse(Rental rental, int totalToPay)
    {
        Rental = rental;
        TotalToPay = totalToPay;
    }
}

