using BancoCajero.Application.Comandos;
using BancoCajero.Application.Seguridad;
using BancoCajero.Domain.Repositorios;
using MediatR;

namespace BancoCajero.Application.Handlers;

public class LoginCommandHandler : IRequestHandler<LoginCommand, string>
{
    private readonly ICuentaBancariaRepository _repositorio;
    private readonly IJwtService _jwt;

    public LoginCommandHandler(ICuentaBancariaRepository repositorio, IJwtService jwt)
    {
        _repositorio = repositorio;
        _jwt = jwt;
    }

    public async Task<string> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var cuenta = await _repositorio.ObtenerPorDniYNombreAsync(request.Dni, request.Nombre);
        if (cuenta is null)
            throw new UnauthorizedAccessException("Credenciales inválidas");

        return _jwt.GenerarToken(request.Dni, request.Nombre);
    }
}
