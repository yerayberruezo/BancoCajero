namespace BancoCajero.Application.Seguridad;

public interface IJwtService
{
    string GenerarToken(string dni, string nombre);
}