using MediatR;

namespace BancoCajero.Application.Consultas;

public class LoginQuery : IRequest<string>
{
    public string Dni { get; }
    public string Nombre { get; }

    public LoginQuery(string dni, string nombre)
    {
        Dni = dni;
        Nombre = nombre;
    }
}
