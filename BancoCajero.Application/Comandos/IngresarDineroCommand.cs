using BancoCajero.Application.DTOs;
using MediatR;

namespace BancoCajero.Application.Comandos;

public class IngresarDineroCommand : IRequest<CuentaDto>
{
    public string NumeroCuenta { get; set; }
    public decimal Cantidad { get; set; }

    public IngresarDineroCommand(string numeroCuenta, decimal cantidad)
    {
        NumeroCuenta = numeroCuenta;
        Cantidad = cantidad;
    }
}
