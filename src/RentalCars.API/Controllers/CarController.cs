﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentalCars.Application.Requests.Car;
using RentalCars.Application.Services.Cars;

namespace RentalCars.API.Controllers;

[ApiController]
[Authorize]
[Route("api/car")]
public class CarController : ControllerBase
{

    #region Properties

    private readonly ICarService _carService;

    #endregion

    #region Constructor

    public CarController(ICarService carService)
    {
        _carService = carService ?? throw new ArgumentNullException(nameof(carService));
    }

    #endregion

    #region GET Methods

    [HttpGet("{id:guid}", Name = "GetByIdAsync")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByIdAsync(Guid id)
    {
        var response = await _carService.GetByIdAsync(id);

        if(response.IsSuccess) return Ok(response);

        return NotFound(response);
    }


    [HttpGet("availables")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<IActionResult> ListAvailableCarsForRental()
    {
        var response = await _carService.ListAvailableCarsForRental();

        if (response.IsSuccess) return Ok(response);

        return NotFound(response);
    }

    [HttpGet("unavailables")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<IActionResult> ListUnavailableCarsForRental()
    {
        var response = await _carService.ListUnavailableCarsForRental();

        if (response.IsSuccess) return Ok(response);

        return NotFound(response);
    }

    #endregion

    #region POST Methods

    [HttpPost("add")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<IActionResult> AddAsync(AddCarRequest newCar)
    {
        var response = await _carService.AddAsync(newCar);

        if (response.IsSuccess) return CreatedAtRoute("GetAsync", new { id = response?.Data?.Id }, response);

        return BadRequest(response);
    }

    #endregion

    #region DELETE Methods

    [HttpDelete("{id:guid}")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<IActionResult> RemoveAsync(Guid id)
    {
        var response = await _carService.RemoveByIdAsync(id);

        if (response.IsSuccess) return NoContent();

        return NotFound(response);
    }

    #endregion

}
