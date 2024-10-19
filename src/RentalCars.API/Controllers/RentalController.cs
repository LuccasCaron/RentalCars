using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentalCars.Application.Requests.Rental;
using RentalCars.Application.Services.Rentals;

namespace RentalCars.API.Controllers;

[ApiController]
[Authorize]
[Route("api/rental")]
public class RentalController : ControllerBase
{

    private readonly IRentalService _rentalService;

    public RentalController(IRentalService rentalService)
    {
        _rentalService = rentalService ?? throw new ArgumentNullException(nameof(rentalService));
    }

    [HttpGet("{id:guid}", Name = "GetAsync")]

    public async Task<IActionResult> GetAsync(Guid id)
    {
        return Ok();
    }

    [HttpPost("add")]

    public async Task<IActionResult> AddAsync(AddRentalRequest newRental)
    {
        var response = await _rentalService.AddAsync(newRental);

        if (response.IsSuccess) return CreatedAtRoute("GetAsync", new { id = response.Data.Id }, response);

        return BadRequest(response);
    }

    [HttpPost("simulate")]

    public async Task<IActionResult> SimulateAsync(AddRentalRequest simulation)
    {
        var response = await _rentalService.SimulateRentalCostAsync(simulation);

        if (response.IsSuccess) return Ok(response);

        return BadRequest(response);
    }

}
