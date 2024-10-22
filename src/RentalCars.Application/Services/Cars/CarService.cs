using Microsoft.EntityFrameworkCore;
using RentalCars.Application.Requests.Car;
using RentalCars.Application.Responses;
using RentalCars.Domain.Entities;
using RentalCars.Infra.Data.Context;

namespace RentalCars.Application.Services.Cars;

public class CarService : ICarService
{

    #region Properties

    private readonly ApplicationDbContext _context;

    #endregion

    #region Constructor

    public CarService(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    #endregion

    #region Methods

    public async Task<ApiResponse<Car>> AddAsync(AddCarRequest newCar)
    {
        var car = Car.Create(newCar.Brand, newCar.Model, newCar.Year, newCar.DailyRentalPrice);

        await _context.Cars.AddAsync(car)
                           .ConfigureAwait(false);

        await _context.SaveChangesAsync()
                      .ConfigureAwait(false);

        return new ApiResponse<Car>(car, 200, "Carro adicionado com sucesso.");
    }

    public async Task<ApiResponse<Car>> GetByIdAsync(Guid id)
    {
        var car = await _context.Cars.FindAsync(id);

        if(car is null) return new ApiResponse<Car>(null, 404, "Carro não encontrado.");

        return new ApiResponse<Car>(car);
    }

    public async Task<ApiResponse<bool>> RemoveByIdAsync(Guid carId)
    {
        var car = await _context.Cars.FindAsync(carId)
                                     .ConfigureAwait(false);

        if (car is null) return new ApiResponse<bool>(false, 404, "Carro não encontrado.");

        var isCarInRental = await _context.Rentals.FirstOrDefaultAsync(x => x.CarId == carId)
                                                  .ConfigureAwait(false);

        if (isCarInRental is not null) return new ApiResponse<bool>(false, 400, "Não é possível deletar um carro que está registrado em alguel.");

        _context.Cars.Remove(car);

        await _context.SaveChangesAsync()
                      .ConfigureAwait(false);

        return new ApiResponse<bool>();
    }

    public async Task<ApiResponse<IEnumerable<Car>>> ListAvailableCarsForRental()
    {
        var listAvailableCarsForRental = await _context.Cars
                                                       .Where(x => x.Availability == true)
                                                       .ToListAsync()
                                                       .ConfigureAwait(false);

        if (listAvailableCarsForRental.Count < 1) return new ApiResponse<IEnumerable<Car>>(null, 404, "Nenhum carro disponível para alugar.");

        return new ApiResponse<IEnumerable<Car>>(listAvailableCarsForRental);
    }

    public async Task<ApiResponse<IEnumerable<Car>>> ListUnavailableCarsForRental()
    {
        var listUnavailableCarsForRental = await _context.Cars
                                                         .Where(x => x.Availability == false)
                                                         .ToListAsync()
                                                         .ConfigureAwait(false);

        if (listUnavailableCarsForRental.Count < 1) return new ApiResponse<IEnumerable<Car>>(null, 404, "Nenhum carro foi alugado.");

        return new ApiResponse<IEnumerable<Car>>(listUnavailableCarsForRental);
    }

    #endregion

}
