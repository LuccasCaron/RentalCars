namespace RentalCars.Domain.Entities.Base;

public abstract class BaseEntity
{

    public Guid Id { get; } = Guid.NewGuid();

}
