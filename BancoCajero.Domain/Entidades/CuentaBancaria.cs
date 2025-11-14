using BancoCajero.Domain.Excepciones;

namespace BancoCajero.Domain.Entidades;

public class CuentaBancaria
{
    public Guid Id { get; private set; }
    public string NumeroCuenta { get; private set; }
    public string Entidad { get; private set; }
    public decimal Saldo { get; private set; }
    public Persona Titular { get; private set; }

    public CuentaBancaria() { }

    public CuentaBancaria(string numeroCuenta, string entidad, Persona titular)
    {
        Id = Guid.NewGuid();
        NumeroCuenta = numeroCuenta;
        Entidad = entidad;
        Saldo = 0;
        Titular = titular;
    }

    public void Ingresar(decimal cantidad)
    {
        if (cantidad <= 0)
            throw new OperacionInvalidaException("La cantidad a ingresar debe ser mayor que cero.");

        if (cantidad > 3000)
            throw new OperacionInvalidaException("No se puede ingresar más de 3000 EUR en una sola operación.");

        Saldo += cantidad;
    }

    public void Retirar(decimal cantidad)
    {
        if (cantidad <= 0)
            throw new OperacionInvalidaException("La cantidad a retirar debe ser mayor que cero.");

        if (cantidad > 1000)
            throw new OperacionInvalidaException("No se puede retirar más de 1000 EUR en una sola operación.");

        if (cantidad > Saldo)
            throw new OperacionInvalidaException("Saldo insuficiente.");

        Saldo -= cantidad;
    }
}
