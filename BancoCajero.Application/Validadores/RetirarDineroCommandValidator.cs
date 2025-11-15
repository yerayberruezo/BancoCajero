using BancoCajero.Application.Comandos;
using FluentValidation;

namespace BancoCajero.Application.Validadores;

public class RetirarDineroCommandValidator : AbstractValidator<RetirarDineroCommand>
{
    public RetirarDineroCommandValidator()
    {
        RuleFor(x => x.NumeroCuenta)
            .NotEmpty().WithMessage("El número de cuenta es obligatorio.")
            .Length(6).WithMessage("El número de cuenta debe tener 6 caracteres.");

        RuleFor(x => x.Cantidad)
            .GreaterThan(0).WithMessage("La cantidad debe ser mayor que cero.")
            .LessThanOrEqualTo(1000).WithMessage("No se puede retirar más de 1000 EUR en una sola operación.");
    }
}
