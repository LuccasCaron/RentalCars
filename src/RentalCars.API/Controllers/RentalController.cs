using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentalCars.Application.Requests.Rentals;
using RentalCars.Application.Services.Rentals;

namespace RentalCars.API.Controllers;

[ApiController]
[Authorize]
[Route("api/rental")]
public class RentalController : ControllerBase
{

    #region Properties

    private readonly IRentalService _rentalService;

    #endregion

    #region Constructor

    public RentalController(IRentalService rentalService)
    {
        _rentalService = rentalService ?? throw new ArgumentNullException(nameof(rentalService));
    }

    #endregion

    #region GET Methods

    [HttpGet("{id:guid}", Name = "GetAsync")]

    public async Task<IActionResult> GetAsync(Guid id)
    {
        var response = await _rentalService.GetRentalByIdAsync(id);

        if (response.IsSuccess) return Ok(response);

        return NotFound(response);
    }

    [HttpGet("user")]

    public async Task<IActionResult> GetRentalsByUserEmail(string userEmail)
    {
        var response = await _rentalService.GetRentalsByUserEmailAsync(userEmail);

        if (response.IsSuccess) return Ok(response);

        return NotFound(response);
    }

    #endregion

    #region POST Methods

    [HttpPost("add")]

    public async Task<IActionResult> AddAsync(AddRentalRequest newRental)
    {
        var response = await _rentalService.AddAsync(newRental);

        if (response.IsSuccess) return CreatedAtRoute("GetAsync", new { id = response.Data.Id }, response);

        return BadRequest(response);
    }

    [HttpPost("simulate")]

    public async Task<IActionResult> SimulateRentalCostAsync(AddRentalRequest simulation)
    {
        var response = await _rentalService.SimulateRentalCostAsync(simulation);

        if (response.IsSuccess) return Ok(response);

        return BadRequest(response);
    }

    [HttpPost("finalize/{id:guid}")]

    public async Task<IActionResult> FinalizeRentalByIdAsyncAsync(Guid id)
    {
        var response = await _rentalService.FinalizeRentalByIdAsync(id);

        if (response.IsSuccess) return Ok(response);

        return BadRequest(response);
    }

    #endregion

    #region PATCH Methods

    [HttpPatch("update/{id:guid}")]

    public async Task<IActionResult> UpdateRentalEndDateByIdAsync(Guid id, DateTime newEndDate)
    {
        var response = await _rentalService.UpdateRentalEndDateByIdAsync(id, newEndDate);

        if (response.IsSuccess) return Ok(response);

        return BadRequest(response);
    }

    #endregion

    #region DELETE Methods

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteByIdAsync(Guid id)
    {
        var response = await _rentalService.DeleteCompletedRentalByIdAsync(id);

        if (response.IsSuccess) return NoContent();

        return BadRequest(response);
    }

    #endregion

}
