using FluentValidation;

namespace RentalCars.Application.Requests.User.Validators;

public class LoginUserRequestValidator : AbstractValidator<LoginUserRequest>
{

    public LoginUserRequestValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("O e-mail é obrigatório.")
            .EmailAddress().WithMessage("O e-mail precisa ser válido.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("A senha é obrigatória.");
    }

}
