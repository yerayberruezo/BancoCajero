using BancoCajero.Application.Comandos;
using BancoCajero.Application.DTOs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BancoCajero.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class CajeroController : ControllerBase
{
    private readonly IMediator _mediator;

    public CajeroController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("ingresar")]
    public async Task<ActionResult<CuentaDto>> Ingresar([FromBody] IngresarDineroCommand comando)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var resultado = await _mediator.Send(comando);
        return Ok(resultado);
    }

    [HttpPost("retirar")]
    public async Task<ActionResult<CuentaDto>> Retirar([FromBody] RetirarDineroCommand comando)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var resultado = await _mediator.Send(comando);
        return Ok(resultado);
    }
}
