using FluentValidation;
using MediatR;

namespace BancoCajero.Application.Comportamientos;

public class ComportamientoValidacion<TPeticion, TRespuesta> : IPipelineBehavior<TPeticion, TRespuesta>
    where TPeticion : notnull
{
    private readonly IEnumerable<IValidator<TPeticion>> _validadores;

    public ComportamientoValidacion(IEnumerable<IValidator<TPeticion>> validadores)
    {
        _validadores = validadores;
    }

    public async Task<TRespuesta> Handle(
        TPeticion peticion,
        RequestHandlerDelegate<TRespuesta> siguiente,
        CancellationToken cancellationToken)
    {
        if (_validadores.Any())
        {
            var contexto = new ValidationContext<TPeticion>(peticion);

            var errores = _validadores
                .Select(v => v.Validate(contexto))
                .SelectMany(resultado => resultado.Errors)
                .Where(f => f != null)
                .ToList();

            if (errores.Count != 0)
                throw new ValidationException(errores);
        }

        return await siguiente();
    }
}
