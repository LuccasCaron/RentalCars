using FluentValidation;

namespace RentalCars.Application.Requests.Car.Validators;

public class AddCarRequestValidator : AbstractValidator<AddCarRequest>
{

    public AddCarRequestValidator()
    {
        RuleFor(x => x.Brand)
           .NotEmpty().WithMessage("A marca é obrigatória.");

        RuleFor(x => x.Model)
            .NotEmpty().WithMessage("O modelo do carro é obrigatório.");

        RuleFor(x => x.Year)
            .InclusiveBetween(1886, DateTime.Now.Year).WithMessage("O ano deve estar entre 1886 e o ano atual.");

        RuleFor(x => x.DailyRentalPrice)
            .NotNull()
            .NotEmpty().WithMessage("O valor diário para aluguel deve ser informado.")
            .GreaterThan(0).WithMessage("O valor diário do aluguel deve ser maior que zero.");
    }

}
