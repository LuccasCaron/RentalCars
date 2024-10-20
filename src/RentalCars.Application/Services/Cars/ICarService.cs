using RentalCars.Application.Requests.Car;
using RentalCars.Application.Responses;
using RentalCars.Domain.Entities;

namespace RentalCars.Application.Services.Cars;

public interface ICarService
{
    Task<ApiResponse<Car>> AddAsync(AddCarRequest newCar);
    Task<ApiResponse<Car>> GetByIdAsync(Guid id);
    Task<ApiResponse<bool>> RemoveByIdAsync(Guid carId);
    Task<ApiResponse<IEnumerable<Car>>> ListAvailableCarsForRental();
    Task<ApiResponse<IEnumerable<Car>>> ListUnavailableCarsForRental();
}