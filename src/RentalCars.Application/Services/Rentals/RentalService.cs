using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RentalCars.Application.Requests.Rentals;
using RentalCars.Application.Responses;
using RentalCars.Application.Responses.Rentals;
using RentalCars.Domain.Entities;
using RentalCars.Infra.Data.Context;

namespace RentalCars.Application.Services.Rentals;

public class RentalService : IRentalService
{

    #region Properties

    private readonly ApplicationDbContext _context;
    private readonly UserManager<IdentityUser> _userManager;

    #endregion

    #region Constructor

    public RentalService(ApplicationDbContext context, UserManager<IdentityUser> userManager)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
    }

    #endregion

    #region Methods

    public async Task<Response<Rental>> AddAsync(AddRentalRequest newRental)
    {
        var carExist = await _context.Cars.FindAsync(newRental.CarId)
                                          .ConfigureAwait(false);

        if (carExist is null) return new Response<Rental>(null, 404, "O id do carro não foi encontrado.");

        if (!carExist.Availability)
            return new Response<Rental>(null, 400, "Este carro não está disponível para alugar.");

        var userExist = await _userManager.FindByEmailAsync(newRental.UserEmail)
                                          .ConfigureAwait(false);

        if (userExist is null) return new Response<Rental>(null, 404, "Usuário não encontrado.");

        var rental = Rental.Create(carExist, Guid.Parse(userExist.Id), newRental.RentalStartDate, newRental.RentalEndDate);

        carExist.SetAvailibilityFalse();

        await _context.Rentals.AddAsync(rental)
                              .ConfigureAwait(false);

        await _context.SaveChangesAsync()
                      .ConfigureAwait(false);

        return new Response<Rental>(rental);
    }

    public async Task<Response<RentalSimulationResponse>> SimulateRentalCostAsync(AddRentalRequest simulation)
    {
        var carExist = await _context.Cars.FindAsync(simulation.CarId)
                                          .ConfigureAwait(false);

        if (carExist is null) return new Response<RentalSimulationResponse>(null, 404, "O id do carro não foi encontrado.");

        var userExist = await _userManager.FindByEmailAsync(simulation.UserEmail)
                                          .ConfigureAwait(false);

        if (userExist is null) return new Response<RentalSimulationResponse>(null, 404, "Usuário não encontrado.");

        var rental = Rental.Create(carExist, Guid.Parse(userExist.Id), simulation.RentalStartDate, simulation.RentalEndDate);

        var priceSimulation = rental.CalculateRentalCost();

        return new Response<RentalSimulationResponse>(new RentalSimulationResponse(priceSimulation));
    }

    public async Task<Response<FinalizedRentalResponse>> FinalizeRentalByIdAsync(Guid id)
    {
        var rental = await _context.Rentals.FindAsync(id)
                                           .ConfigureAwait(false);

        if (rental is null) return new Response<FinalizedRentalResponse>(null, 404, "Aluguel não encontrado.");

        if (rental.IsCompleted) return new Response<FinalizedRentalResponse>(null, 400, "O Aluguel já foi encerrado.");

        rental.FinalizeRental();

        int totalToPay = rental.CalculateTotalToPay(DateTime.Now);

        var response = new FinalizedRentalResponse(rental, totalToPay);

        return new Response<FinalizedRentalResponse>(response);
    }

    public async Task<Response<Rental>> UpdateRentalEndDateByIdAsync(Guid id, DateTime newEndDate)
    {
        var rental = await _context.Rentals.FindAsync(id)
                                         .ConfigureAwait(false);

        if (rental is null) return new Response<Rental>(null, 404, "Aluguel não encontrado.");

        if (rental.IsCompleted) return new Response<Rental>(null, 400, "O Aluguel já foi encerrado.");

        rental.UpdateRentalEndDate(newEndDate);

        return new Response<Rental>(rental, 200, "Data atualizada com sucesso.");

    }

    public async Task<Response<Rental>> GetRentalByIdAsync(Guid id)
    {
        var rental = await _context.Rentals.FindAsync(id)
                                           .ConfigureAwait(false);

        if (rental is null) return new Response<Rental>(null, 404, "Aluguel não encontrado");

        return new Response<Rental>(rental);
    }

    public async Task<Response<IEnumerable<Rental>>> GetRentalsByUserEmailAsync(string userEmail)
    {

        var existUser = await _userManager.FindByIdAsync(userEmail)
                                          .ConfigureAwait(false);

        if(existUser is null) return new Response<IEnumerable<Rental>>(null, 404, "Usuário não encontrado.");

        var rentals = await _context.Rentals.Where(x => x.UserId == Guid.Parse(existUser.Id))
                                            .ToListAsync()
                                            .ConfigureAwait(false);

        if (rentals is null) return new Response<IEnumerable<Rental>>(null, 404, "Este usuário ainda não alugou nenhum carro.");

        return new Response<IEnumerable<Rental>>(rentals);
    }

    #endregion

}
