namespace RentalCars.Application.Requests.Car;

public class AddCarRequest
{

    public string Brand { get; set; } = string.Empty;

    public string Model { get; set; } = string.Empty;

    public int Year { get; set; }

    public int DailyRentalPrice { get; set; }

}
