using RentalCars.Domain.Entities;

namespace RentalCars.Infra.Email.DTOs;

public class RentalFinishDto
{

    public RentalFinishDto(string nome, Rental rental, Car car, int totalToPay)
    {
        Nome = nome;
        Rental = rental;
        Car = car;
        TotalToPay = totalToPay;
    }

    public string Nome { get; set; }

    public Rental Rental { get; set; }

    public Car Car { get; set; }

    public int TotalToPay { get; set; }

}
