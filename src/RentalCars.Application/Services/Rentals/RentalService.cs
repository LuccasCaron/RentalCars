using MassTransit;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RentalCars.Application.Requests.Rentals;
using RentalCars.Application.Responses;
using RentalCars.Application.Responses.Rentals;
using RentalCars.Application.Services.Publishers;
using RentalCars.Domain.Entities;
using RentalCars.Infra.Data.Context;

namespace RentalCars.Application.Services.Rentals;

public class RentalService : IRentalService
{

    #region Properties

    private readonly ApplicationDbContext _context;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IRentalPublisherService _publisherService;

    #endregion

    #region Constructor

    public RentalService(ApplicationDbContext context, UserManager<IdentityUser> userManager, IRentalPublisherService publisherService)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        _publisherService = publisherService ?? throw new ArgumentNullException(nameof(publisherService));
    }

    #endregion

    #region Methods

    public async Task<ApiResponse<Rental>> AddAsync(AddRentalRequest newRental)
    {
        var carExist = await _context.Cars.FindAsync(newRental.CarId)
                                          .ConfigureAwait(false);

        if (carExist is null) return new ApiResponse<Rental>(null, 404, "O id do carro não foi encontrado.");

        if (!carExist.Availability)
            return new ApiResponse<Rental>(null, 400, "Este carro não está disponível para alugar.");

        var userExist = await _userManager.FindByEmailAsync(newRental.UserEmail)
                                          .ConfigureAwait(false);

        if (userExist is null) return new ApiResponse<Rental>(null, 404, "Usuário não encontrado.");

        var existRental = await _context.Rentals.FirstOrDefaultAsync(x => x.UserId == Guid.Parse(userExist.Id) && x.IsCompleted == false);

        if (existRental is not null) return new ApiResponse<Rental>(null, 400, "Usuário já está com um alguel ativo.");

        var rental = Rental.Create(carExist, Guid.Parse(userExist.Id), newRental.RentalStartDate, newRental.RentalEndDate);

        carExist.SetAvailibilityFalse();

        await _context.Rentals.AddAsync(rental)
                              .ConfigureAwait(false);

        await _context.SaveChangesAsync()
                      .ConfigureAwait(false);

        await _publisherService.PublishRentalCreatedAsync(rental.Id);

        return new ApiResponse<Rental>(rental);
    }

    public async Task<ApiResponse<RentalSimulationResponse>> SimulateRentalCostAsync(AddRentalRequest simulation)
    {
        var carExist = await _context.Cars.FindAsync(simulation.CarId)
                                          .ConfigureAwait(false);

        if (carExist is null) return new ApiResponse<RentalSimulationResponse>(null, 404, "O id do carro não foi encontrado.");

        var userExist = await _userManager.FindByEmailAsync(simulation.UserEmail)
                                          .ConfigureAwait(false);

        if (userExist is null) return new ApiResponse<RentalSimulationResponse>(null, 404, "Usuário não encontrado.");

        var rental = Rental.Create(carExist, Guid.Parse(userExist.Id), simulation.RentalStartDate, simulation.RentalEndDate);

        var priceSimulation = rental.CalculateRentalCost(DateTime.Now);

        return new ApiResponse<RentalSimulationResponse>(new RentalSimulationResponse(priceSimulation));
    }

    public async Task<ApiResponse<FinalizedRentalResponse>> FinalizeRentalByIdAsync(Guid id)
    {
        var rental = await _context.Rentals.FindAsync(id)
                                           .ConfigureAwait(false);

        if (rental is null) return new ApiResponse<FinalizedRentalResponse>(null, 404, "Aluguel não encontrado.");

        if (rental.IsCompleted) return new ApiResponse<FinalizedRentalResponse>(null, 400, "O Aluguel já foi encerrado.");

        var car = await _context.Cars.FindAsync(rental.CarId);

        car?.SetAvailibilityTrue();

        rental.FinalizeRental();

        await _context.SaveChangesAsync()
              .ConfigureAwait(false);

        var totalToPay = rental.CalculateTotalToPay(DateTime.Now);

        var response = new FinalizedRentalResponse(rental, totalToPay);

        return new ApiResponse<FinalizedRentalResponse>(response);
    }

    public async Task<ApiResponse<Rental>> UpdateRentalEndDateByIdAsync(Guid id, DateTime newEndDate)
    {
        var rental = await _context.Rentals.FindAsync(id)
                                         .ConfigureAwait(false);

        if (rental is null) return new ApiResponse<Rental>(null, 404, "Aluguel não encontrado.");

        if (rental.IsCompleted) return new ApiResponse<Rental>(null, 400, "O Aluguel já foi encerrado.");

        rental.UpdateRentalEndDate(newEndDate);

        return new ApiResponse<Rental>(rental, 200, "Data atualizada com sucesso.");

    }

    public async Task<ApiResponse<Rental>> GetRentalByIdAsync(Guid id)
    {
        var rental = await _context.Rentals.FindAsync(id)
                                           .ConfigureAwait(false);

        if (rental is null) return new ApiResponse<Rental>(null, 404, "Aluguel não encontrado");

        return new ApiResponse<Rental>(rental);
    }

    public async Task<ApiResponse<IEnumerable<Rental>>> GetRentalsByUserEmailAsync(string userEmail)
    {

        var existUser = await _userManager.FindByEmailAsync(userEmail)
                                          .ConfigureAwait(false);

        if (existUser is null) return new ApiResponse<IEnumerable<Rental>>(null, 404, "Usuário não encontrado.");

        var rentals = await _context.Rentals.Where(x => x.UserId == Guid.Parse(existUser.Id))
                                            .ToListAsync()
                                            .ConfigureAwait(false);

        if (rentals.Count < 1) return new ApiResponse<IEnumerable<Rental>>(null, 404, "Este usuário ainda não alugou nenhum carro.");

        return new ApiResponse<IEnumerable<Rental>>(rentals);
    }

    #endregion

}
