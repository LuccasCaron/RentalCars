using RentalCars.Domain.Entities;

namespace RentalCars.Infra.Email.DTOs;

public class NewRentalDto
{

    public NewRentalDto(string nome, Rental rental, Car car)
    {
        Nome = nome;
        Rental = rental;
        Car = car;
    }

    public string Nome { get; set; }

    public Rental Rental { get; set; }

    public Car Car { get; set; }

}
