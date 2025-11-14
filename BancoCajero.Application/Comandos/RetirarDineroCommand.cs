using BancoCajero.Application.DTOs;
using MediatR;

namespace BancoCajero.Application.Comandos;

public class RetirarDineroCommand : IRequest<CuentaDto>
{
    public string NumeroCuenta { get; set; }
    public decimal Cantidad { get; set; }

    public RetirarDineroCommand(string numeroCuenta, decimal cantidad)
    {
        NumeroCuenta = numeroCuenta;
        Cantidad = cantidad;
    }
}
