namespace RentalCars.Application.Requests.User;

public class AddUserRequest
{

    public string Email { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

    public string ConfirmPassword { get; set; } = string.Empty;

}
