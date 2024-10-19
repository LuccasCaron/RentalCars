using FluentValidation;

namespace RentalCars.Application.Requests.User.Validators;

public class AddUserRequestValidator : AbstractValidator<AddUserRequest>
{
    public AddUserRequestValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("O e-mail é obrigatório.")
            .EmailAddress().WithMessage("O e-mail precisa ser válido.");

        RuleFor(x => x.Password)
              .NotEmpty().WithMessage("A senha é obrigatória.")
              .MinimumLength(6).WithMessage("A senha precisa ter no mínimo 6 caracteres.")
              .Matches("[A-Z]").WithMessage("A senha precisa conter ao menos uma letra maiúscula.")
              .Matches("[a-z]").WithMessage("A senha precisa conter ao menos uma letra minúscula.")
              .Matches("[0-9]").WithMessage("A senha precisa conter ao menos um número.")
              .Matches("[^a-zA-Z0-9]").WithMessage("A senha precisa conter ao menos um caractere especial.");

        RuleFor(x => x.ConfirmPassword)
            .NotEmpty().WithMessage("Confirmar a senha é obrigatório.") 
            .Equal(x => x.Password).WithMessage("As senhas devem coincidir."); 
    }
}
