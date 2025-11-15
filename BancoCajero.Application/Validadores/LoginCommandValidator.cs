using BancoCajero.Application.Comandos;
using FluentValidation;

namespace BancoCajero.Application.Validadores;

public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(x => x.Dni)
            .NotEmpty().WithMessage("El DNI es obligatorio.")
            .Matches("^[0-9]{8}[A-Za-z]$").WithMessage("El DNI debe tener 8 números y una letra.");

        RuleFor(x => x.Nombre)
            .NotEmpty().WithMessage("El nombre es obligatorio.")
            .MinimumLength(2).WithMessage("El nombre debe tener al menos 2 caracteres.")
            .MaximumLength(50).WithMessage("El nombre no puede superar los 50 caracteres.");
    }
}
