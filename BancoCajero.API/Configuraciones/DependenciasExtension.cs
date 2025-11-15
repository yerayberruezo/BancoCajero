using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using BancoCajero.Domain.Repositorios;
using BancoCajero.Infrastructure.Persistencia.Repositorios;
using BancoCajero.Infrastructure.Persistencia;
using BancoCajero.Application.Comandos;
using BancoCajero.Infrastructure.Seguridad;
using BancoCajero.Application.Seguridad;
using Microsoft.IdentityModel.Tokens;
using BancoCajero.Application.Comportamientos;
using System.Text;

namespace BancoCajero.API.Configuraciones;

public static class DependenciasExtension
{
    public static IServiceCollection AgregarDependencias(this IServiceCollection servicios, IConfiguration config)
    {
        servicios.AddDbContext<CajeroDbContext>(options =>
            options.UseSqlServer(config.GetConnectionString("CajeroConnection")));

        servicios.AddScoped<ICuentaBancariaRepository, CuentaBancariaRepository>();

        servicios.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(IngresarDineroCommand).Assembly);
            cfg.RegisterServicesFromAssembly(typeof(ComportamientoValidacion<,>).Assembly);
        });

        servicios.AddValidatorsFromAssembly(typeof(IngresarDineroCommand).Assembly);

        servicios.AddTransient(typeof(IPipelineBehavior<,>), typeof(ComportamientoValidacion<,>));

        servicios.AddHttpContextAccessor();

        servicios.AddScoped<IJwtService, JwtService>();

        servicios.AddAuthentication("Bearer")
            .AddJwtBearer("Bearer", options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = config["Jwt:Issuer"],
                    ValidAudience = config["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(config["Jwt:Key"]!)
                    )
                };
            });

        servicios.AddAuthorization();

        return servicios;
    }
}
