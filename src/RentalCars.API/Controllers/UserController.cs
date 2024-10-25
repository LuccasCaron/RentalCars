using Microsoft.AspNetCore.Mvc;
using RentalCars.Application.Requests.User;
using RentalCars.Application.Services.User;

namespace RentalCars.API.Controllers;

[ApiController]
[Route("api/user")]
public class UserController : ControllerBase
{

    #region Properties

    private readonly IUserService _userService;

    #endregion

    #region Constructor

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    #endregion

    #region POST Methods

    [HttpPost("add")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<IActionResult> AddAsync(AddUserRequest newUser)
    {
        var response = await _userService.AddAsync(newUser);

        if (!response.IsSuccess) return BadRequest(response);

        return Ok(response);
    }

    [HttpPost("login")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<IActionResult> LoginAsync(LoginUserRequest credentials)
    {

        var response = await _userService.LoginAsync(credentials);

        if (!response.IsSuccess) return BadRequest(response);

        return Ok(response);

    }

    #endregion

}
