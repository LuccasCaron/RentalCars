namespace RentalCars.Application.Responses.Rentals;

public class RentalSimulationResponse
{
    public int EstimatedCost { get; set; }

    public RentalSimulationResponse(int estimatedCost)
    {
        EstimatedCost = estimatedCost;
    }
}

