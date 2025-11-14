using BancoCajero.Domain.Entidades;

namespace BancoCajero.Domain.Repositorios;

public interface ICuentaBancariaRepository
{
    Task<CuentaBancaria?> ObtenerPorNumeroCuentaAsync(string numeroCuenta);

    Task<CuentaBancaria?> ObtenerPorDniYNombreAsync(string dni, string nombre);

    Task GuardarAsync(CuentaBancaria cuenta);
}
