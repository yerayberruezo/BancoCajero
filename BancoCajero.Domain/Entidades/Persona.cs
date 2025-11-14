namespace BancoCajero.Domain.Entidades;

public class Persona
{
    public string Dni { get; private set; }
    public string Nombre { get; private set; }
    public string Apellidos { get; private set; }

    public Persona() { }

    public Persona(string dni, string nombre, string apellidos)
    {
        Dni = dni;
        Nombre = nombre;
        Apellidos = apellidos;
    }
}
