using BancoCajero.Application.Comandos;
using BancoCajero.Application.DTOs;
using BancoCajero.Domain.Excepciones;
using BancoCajero.Domain.Repositorios;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace BancoCajero.Application.Handlers;

public class IngresarDineroHandler : IRequestHandler<IngresarDineroCommand, CuentaDto>
{
    private readonly ICuentaBancariaRepository _repositorio;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public IngresarDineroHandler(ICuentaBancariaRepository repositorio, IHttpContextAccessor httpContextAccessor)
    {
        _repositorio = repositorio;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<CuentaDto> Handle(IngresarDineroCommand request, CancellationToken cancellationToken)
    {
        var cuenta = await _repositorio.ObtenerPorNumeroCuentaAsync(request.NumeroCuenta);
        if (cuenta is null)
            throw new OperacionInvalidaException("Cuenta no encontrada.");

        var dniUsuario = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (cuenta.Titular.Dni != dniUsuario)
            throw new UnauthorizedAccessException("No puedes ingresar dinero en una cuenta que no es tuya.");

        cuenta.Ingresar(request.Cantidad);
        await _repositorio.GuardarAsync(cuenta);

        return new CuentaDto(cuenta.NumeroCuenta, cuenta.Saldo);
    }
}
