using BancoCajero.Application.Comandos;
using BancoCajero.Application.Handlers;
using BancoCajero.Application.DTOs;
using BancoCajero.Domain.Entidades;
using BancoCajero.Domain.Repositorios;
using FluentAssertions;
using Moq;
using Xunit;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace BancoCajero.Tests.Application;

public class IngresarDineroHandlerTests
{
    private readonly Mock<ICuentaBancariaRepository> _mockRepo;
    private readonly Mock<IHttpContextAccessor> _mockHttpContext;
    private readonly IngresarDineroHandler _handler;

    public IngresarDineroHandlerTests()
    {
        _mockRepo = new Mock<ICuentaBancariaRepository>();
        _mockHttpContext = new Mock<IHttpContextAccessor>();

        var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
            new Claim(ClaimTypes.NameIdentifier, "98765432A")
        }, "mock"));

        var httpContext = new DefaultHttpContext { User = user };
        _mockHttpContext.Setup(x => x.HttpContext).Returns(httpContext);

        _handler = new IngresarDineroHandler(_mockRepo.Object, _mockHttpContext.Object);
    }

    [Fact]
    public async Task IngresarDinero_Deberia_AgregarSaldoSiEsValido()
    {
        var persona = new Persona("98765432A", "Yeray", "Berruezo");
        var cuenta = new CuentaBancaria("ES999", "BBVA", persona);
        var comando = new IngresarDineroCommand("ES999", 500);

        _mockRepo.Setup(r => r.ObtenerPorNumeroCuentaAsync("ES999"))
                 .ReturnsAsync(cuenta);

        var resultado = await _handler.Handle(comando, CancellationToken.None);

        resultado.Saldo.Should().Be(500);
        resultado.NumeroCuenta.Should().Be("ES999");

        _mockRepo.Verify(r => r.GuardarAsync(It.IsAny<CuentaBancaria>()), Times.Once);
    }

    [Fact]
    public async Task IngresarDinero_Deberia_LanzarExcepcionSiSuperaLimite()
    {
        var persona = new Persona("98765432A", "Yeray", "Berruezo");
        var cuenta = new CuentaBancaria("ES999", "BBVA", persona);
        var comando = new IngresarDineroCommand("ES999", 5000);

        _mockRepo.Setup(r => r.ObtenerPorNumeroCuentaAsync("ES999"))
                 .ReturnsAsync(cuenta);

        Func<Task> accion = async () => await _handler.Handle(comando, CancellationToken.None);

        await accion.Should().ThrowAsync<Exception>()
            .WithMessage("No se puede ingresar más de 3000 EUR en una sola operación.");

        _mockRepo.Verify(r => r.GuardarAsync(It.IsAny<CuentaBancaria>()), Times.Never);
    }
}
