using RentalCars.Application.Requests.Rental;
using RentalCars.Application.Responses;
using RentalCars.Domain.Entities;

namespace RentalCars.Application.Services.Rentals
{
    public interface IRentalService
    {
        Task<Response<Rental>> AddAsync(AddRentalRequest newRental);
        Task<Response<Rental>> EndRentalByIdAsync(Guid id);
        Task<Response<IEnumerable<Rental>>> GetRentalsByUserIdAsync();
        Task<Response<Rental>> SimulateRentalCostAsync(AddRentalRequest simulation);
        Task<Response<Rental>> UpdateRentalEndDateByIdAsync(DateTime endDate);
    }
}