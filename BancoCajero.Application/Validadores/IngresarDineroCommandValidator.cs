using BancoCajero.Application.Comandos;
using FluentValidation;

namespace BancoCajero.Application.Validadores;

public class IngresarDineroCommandValidator : AbstractValidator<IngresarDineroCommand>
{
    public IngresarDineroCommandValidator()
    {
        RuleFor(x => x.NumeroCuenta)
            .NotEmpty().WithMessage("El número de cuenta es obligatorio.")
            .Length(6).WithMessage("El número de cuenta debe tener 6 caracteres.");

        RuleFor(x => x.Cantidad)
            .GreaterThan(0).WithMessage("La cantidad debe ser mayor que cero.")
            .LessThanOrEqualTo(3000).WithMessage("No se puede ingresar más de 3000 EUR en una sola operación.");
    }
}
