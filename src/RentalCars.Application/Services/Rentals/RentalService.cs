using Microsoft.AspNetCore.Identity;
using RentalCars.Application.Requests.Rental;
using RentalCars.Application.Responses;
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

    public async Task<Response<Rental>> SimulateRentalCostAsync(AddRentalRequest simulation)
    {
        var carExist = await _context.Cars.FindAsync(simulation.CarId)
                                          .ConfigureAwait(false);

        if (carExist is null) return new Response<Rental>(null, 404, "O id do carro não foi encontrado.");

        var userExist = await _userManager.FindByEmailAsync(simulation.UserEmail)
                                          .ConfigureAwait(false); 

        if (userExist is null) return new Response<Rental>(null, 404, "Usuário não encontrado.");

        var rental = Rental.Create(carExist, Guid.Parse(userExist.Id), simulation.RentalStartDate, simulation.RentalEndDate);

        var priceSimulation = rental.CalculateRentalCost();

        return new Response<Rental>(rental, 200, $"O Valor da simulação ficaria a partir de {priceSimulation}");
    }

    public async Task<Response<Rental>> EndRentalByIdAsync(Guid id)
    {
        return new Response<Rental>();
    }

    public async Task<Response<Rental>> UpdateRentalEndDateByIdAsync(DateTime endDate)
    {
        return new Response<Rental>();
    }

    public async Task<Response<IEnumerable<Rental>>> GetRentalsByUserIdAsync()
    {
        return new Response<IEnumerable<Rental>>();
    }

}
