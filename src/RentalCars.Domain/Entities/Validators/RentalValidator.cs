using FluentValidation;

namespace RentalCars.Domain.Entities.Validators;

internal class RentalValidator : AbstractValidator<Rental>
{
    public RentalValidator()
    {
        RuleFor(x => x.CarId)
            .NotEmpty().WithMessage("O carro é obrigatório para o aluguel.");

        RuleFor(x => x.Car)
            .NotNull().WithMessage("A referência ao carro é obrigatória.");

        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("O usuário é obrigatório para o aluguel.");

        RuleFor(x => x.InitDate)
            .NotEmpty().WithMessage("A data de início do aluguel é obrigatória.")
            .LessThanOrEqualTo(DateTime.Now).WithMessage("A data de início do aluguel não pode ser no futuro.");

        RuleFor(x => x.FinalDate)
            .NotEmpty().WithMessage("A data final do aluguel é obrigatória.")
            .GreaterThan(x => x.InitDate).WithMessage("A data final do aluguel deve ser maior que a data de início.");

        RuleFor(x => x.AppliedDailyPrice)
            .GreaterThan(0).WithMessage("O valor diário do aluguel deve ser maior que zero.");

        RuleFor(x => x.FineAmount)
            .GreaterThanOrEqualTo(0).WithMessage("O valor da multa por atraso não pode ser negativo.");
    }
}
