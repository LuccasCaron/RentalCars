using Microsoft.AspNetCore.Identity;
using RentalCars.Application.Requests.User;
using RentalCars.Application.Responses;
using RentalCars.Application.Services.Jwt;

namespace RentalCars.Application.Services.User;

public class UserService : IUserService
{

    #region Properties

    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IJwtService _jwtService;

    #endregion

    #region Constructor

    public UserService(
       SignInManager<IdentityUser> signInManager,
       UserManager<IdentityUser> userManager,
       IJwtService jwtService
  )
    {
        _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        _jwtService = jwtService ?? throw new ArgumentNullException(nameof(_jwtService));
    }

    #endregion

    #region Methods

    public async Task<Response<IdentityUser>> AddAsync(AddUserRequest newUser)
    {
        var user = new IdentityUser
        {
            UserName = newUser.Email,
            Email = newUser.Email,
            EmailConfirmed = true
        };

        var result = await _userManager.CreateAsync(user, newUser.Password);

        if (result.Succeeded)
        {
            await _signInManager.SignInAsync(user, false);

            return new Response<IdentityUser>(user, 200);
        }

        return new Response<IdentityUser>(null, 400, "Falha ao criar usuário.");
    }

    public async Task<Response<string>> LoginAsync(LoginUserRequest credentials)
    {
        var result = await _signInManager.PasswordSignInAsync(credentials.Email, credentials.Password, false, false);

        if(!result.Succeeded) return new Response<string>(null, 400, "Usuário ou senha incorretos.");

        var token = await _jwtService.GenerateTokenAsync(credentials.Email);

        if (!result.Succeeded) return new Response<string>(null, 400, "Erro ao gerar Token.");

        return new Response<string>(token);

    }

    #endregion

}
