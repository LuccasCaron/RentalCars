namespace RentalCars.Application.Requests.Rental;

public class AddRentalRequest
{

    public Guid CarId { get; set; }

    public string UserEmail { get; set; } = string.Empty;

    public DateTime RentalStartDate { get; set; }

    public DateTime RentalEndDate { get; set; }

}
