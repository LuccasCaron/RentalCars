using RentalCars.Application.Requests.Car;
using RentalCars.Application.Responses;
using RentalCars.Domain.Entities;

namespace RentalCars.Application.Services.Cars;

public interface ICarService
{
    Task<Response<Car>> AddAsync(AddCarRequest newCar);
    Task<Response<Car>> GetByIdAsync(Guid id);
    Task<Response<bool>> RemoveByIdAsync(Guid carId);
    Task<Response<IEnumerable<Car>>> ListAvailableCarsForRental();
    Task<Response<IEnumerable<Car>>> ListUnavailableCarsForRental();
}