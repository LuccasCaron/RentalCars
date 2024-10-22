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

        RuleFor(x => x.RentalStartDate)
            .NotEmpty().WithMessage("A data de início do aluguel é obrigatória.")
            .Must(date => date.Date >= DateTime.Now.Date)
            .WithMessage("A data de início do aluguel precisa ser com pelo menos 1 dia de antecedência.");


        RuleFor(x => x.RentalEndDate)
            .NotEmpty().WithMessage("A data final do aluguel é obrigatória.")
            .GreaterThan(x => x.RentalStartDate).WithMessage("A data final do aluguel deve ser maior que a data de início.");

        RuleFor(x => x.IsCompleted)
            .NotNull().WithMessage("A propriedade IsCompleted deve ser especificada.")
            .Must(avail => avail == true || avail == false).WithMessage("A propriedade IsCompleted deve ser verdadeira ou falsa.");

        RuleFor(x => x.AppliedDailyPrice)
            .GreaterThan(0).WithMessage("O valor diário do aluguel deve ser maior que zero.");

        RuleFor(x => x.FineAmount)
            .GreaterThanOrEqualTo(0).WithMessage("O valor da multa por atraso não pode ser negativo.");
    }
}
