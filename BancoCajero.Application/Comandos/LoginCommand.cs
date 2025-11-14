using MediatR;

namespace BancoCajero.Application.Comandos;

public class LoginCommand : IRequest<string>
{
    public string Dni { get; set; } = string.Empty;
    public string Nombre { get; set; } = string.Empty;
}
