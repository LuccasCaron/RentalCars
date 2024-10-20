using RentalCars.Application.Requests.Rentals;
using RentalCars.Application.Responses;
using RentalCars.Application.Responses.Rentals;
using RentalCars.Domain.Entities;

namespace RentalCars.Application.Services.Rentals;

public interface IRentalService
{
    Task<ApiResponse<Rental>> AddAsync(AddRentalRequest newRental);
    Task<ApiResponse<FinalizedRentalResponse>> FinalizeRentalByIdAsync(Guid id);
    Task<ApiResponse<Rental>> GetRentalByIdAsync(Guid id);
    Task<ApiResponse<IEnumerable<Rental>>> GetRentalsByUserEmailAsync(string userEmail);
    Task<ApiResponse<RentalSimulationResponse>> SimulateRentalCostAsync(AddRentalRequest simulation);
    Task<ApiResponse<Rental>> UpdateRentalEndDateByIdAsync(Guid id, DateTime newEndDate);
}