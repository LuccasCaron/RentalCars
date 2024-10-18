namespace RentalCars.Domain.Entities.Base;

public class BaseEntity
{

    public Guid Id { get; } = Guid.NewGuid();

}
