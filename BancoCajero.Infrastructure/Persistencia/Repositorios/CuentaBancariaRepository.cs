using BancoCajero.Domain.Entidades;
using BancoCajero.Domain.Repositorios;
using BancoCajero.Infrastructure.Persistencia;
using Microsoft.EntityFrameworkCore;

namespace BancoCajero.Infrastructure.Persistencia.Repositorios;

public class CuentaBancariaRepository : ICuentaBancariaRepository
{
    private readonly CajeroDbContext _context;

    public CuentaBancariaRepository(CajeroDbContext context)
    {
        _context = context;
    }
    public async Task<CuentaBancaria?> ObtenerPorDniYNombreAsync(string dni, string nombre)
    {
        return await _context.Cuentas
            .FirstOrDefaultAsync(c => c.Titular.Dni == dni && c.Titular.Nombre == nombre);
    }


    public async Task<CuentaBancaria?> ObtenerPorNumeroCuentaAsync(string numeroCuenta)
    {
        return await _context.Cuentas
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.NumeroCuenta == numeroCuenta);
    }

    public async Task GuardarAsync(CuentaBancaria cuenta)
    {
        _context.Update(cuenta);
        await _context.SaveChangesAsync();
    }
}
