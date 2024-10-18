using FluentValidation;

namespace RentalCars.Domain.Entities.Validators;

internal class CarValidator : AbstractValidator<Car>
{

    public CarValidator()
    {
        RuleFor(x => x.Brand)
            .NotEmpty().WithMessage("A marca é obrigatória.");

        RuleFor(x => x.Model)
            .NotEmpty().WithMessage("O modelo do carro é obrigatório.");

        RuleFor(x => x.Year)
            .InclusiveBetween(1886, DateTime.Now.Year).WithMessage("O ano deve estar entre 1886 e o ano atual.");

        RuleFor(x => x.Availability)
            .NotNull().WithMessage("A disponibilidade deve ser especificada.")
            .Must(avail => avail == true || avail == false).WithMessage("A disponibilidade deve ser verdadeira ou falsa.");
    }

}
