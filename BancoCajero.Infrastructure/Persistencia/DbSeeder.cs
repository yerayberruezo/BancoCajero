using BancoCajero.Domain.Entidades;
using Microsoft.EntityFrameworkCore;

namespace BancoCajero.Infrastructure.Persistencia;

public static class DbSeeder
{
    public static async Task InicializarAsync(CajeroDbContext context)
    {
        // Aplicar migraciones pendientes
        await context.Database.MigrateAsync();

        // Verificar si ya hay cuentas
        if (await context.Cuentas.AnyAsync())
            return;

        var cuentas = new List<CuentaBancaria>
        {
            new CuentaBancaria("ES0001", "BBVA", new Persona("98765432A", "Yeray", "Berruezo")),
            new CuentaBancaria("ES0002", "Santander", new Persona("22222222B", "Alvaro", "Alonso")),
            new CuentaBancaria("ES0003", "Caixa", new Persona("33333333C", "Pedro Pablo", "Esteve"))
        };

        // Cargar saldos de ejemplo
        cuentas[0].Ingresar(1500);
        cuentas[1].Ingresar(2000);
        cuentas[2].Ingresar(2500);

        await context.Cuentas.AddRangeAsync(cuentas);
        await context.SaveChangesAsync();
    }
}
