using BancoCajero.Application.Comandos;
using BancoCajero.Application.DTOs;
using BancoCajero.Domain.Excepciones;
using BancoCajero.Domain.Repositorios;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace BancoCajero.Application.Handlers;

public class RetirarDineroHandler : IRequestHandler<RetirarDineroCommand, CuentaDto>
{
    private readonly ICuentaBancariaRepository _repositorio;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public RetirarDineroHandler(ICuentaBancariaRepository repositorio, IHttpContextAccessor httpContextAccessor)
    {
        _repositorio = repositorio;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<CuentaDto> Handle(RetirarDineroCommand request, CancellationToken cancellationToken)
    {
        var cuenta = await _repositorio.ObtenerPorNumeroCuentaAsync(request.NumeroCuenta);
        if (cuenta is null)
            throw new OperacionInvalidaException("Cuenta no encontrada.");

        var dniUsuario = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (cuenta.Titular.Dni != dniUsuario)
            throw new UnauthorizedAccessException("No puedes retirar dinero de una cuenta que no es tuya.");

        cuenta.Retirar(request.Cantidad);
        await _repositorio.GuardarAsync(cuenta);

        return new CuentaDto(cuenta.NumeroCuenta, cuenta.Saldo);
    }
}
