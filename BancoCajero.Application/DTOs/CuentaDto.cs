namespace BancoCajero.Application.DTOs;

public class CuentaDto
{
    public string NumeroCuenta { get; set; }
    public decimal Saldo { get; set; }

    public CuentaDto(string numeroCuenta, decimal saldo)
    {
        NumeroCuenta = numeroCuenta;
        Saldo = saldo;
    }
}
