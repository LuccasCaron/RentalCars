using FluentValidation;

namespace RentalCars.Application.Requests.Rental.Validators;

public class AddRentalRequestValidator : AbstractValidator<AddRentalRequest>
{

    public AddRentalRequestValidator()
    {
        RuleFor(x => x.CarId)
            .NotEmpty().WithMessage("O id do carro não pode ser vazio.");

        RuleFor(x => x.UserEmail)
            .NotEmpty().WithMessage("O id do usuario não pode ser vazio.")
            .EmailAddress().WithMessage("O endereço de e-mail precisa ser valído.");

        RuleFor(x => x.RentalStartDate)
           .NotEmpty().WithMessage("A data de início do aluguel é obrigatória.")
           .GreaterThanOrEqualTo(DateTime.Now).WithMessage("A data de início do aluguel não pode ser no passado.");

        RuleFor(x => x.RentalEndDate)
            .NotEmpty().WithMessage("A data final do aluguel é obrigatória.")
            .GreaterThan(x => x.RentalStartDate).WithMessage("A data final do aluguel deve ser maior que a data de início.");
    }

}
