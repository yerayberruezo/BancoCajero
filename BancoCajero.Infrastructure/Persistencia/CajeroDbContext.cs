using BancoCajero.Domain.Entidades;
using Microsoft.EntityFrameworkCore;

namespace BancoCajero.Infrastructure.Persistencia;

public class CajeroDbContext : DbContext
{
    public CajeroDbContext(DbContextOptions<CajeroDbContext> options) : base(options) { }

    public DbSet<CuentaBancaria> Cuentas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CuentaBancaria>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Saldo)
                  .HasPrecision(18, 2); 

            entity.OwnsOne(e => e.Titular, titular =>
            {
                titular.Property(t => t.Dni).IsRequired();
                titular.Property(t => t.Nombre).IsRequired();
                titular.Property(t => t.Apellidos).IsRequired();
            });
        });

    }
}
