using RentalCars.Application.Requests.Rentals;
using RentalCars.Application.Responses;
using RentalCars.Application.Responses.Rentals;
using RentalCars.Domain.Entities;

namespace RentalCars.Application.Services.Rentals;

public interface IRentalService
{
    Task<Response<Rental>> AddAsync(AddRentalRequest newRental);
    Task<Response<FinalizedRentalResponse>> FinalizeRentalByIdAsync(Guid id);
    Task<Response<Rental>> GetRentalByIdAsync(Guid id);
    Task<Response<IEnumerable<Rental>>> GetRentalsByUserEmailAsync(string userEmail);
    Task<Response<RentalSimulationResponse>> SimulateRentalCostAsync(AddRentalRequest simulation);
    Task<Response<Rental>> UpdateRentalEndDateByIdAsync(Guid id, DateTime newEndDate);
}