namespace Adres.Domain.Entities;

public class Proveedor
{
    public int Id { get; set; }
    public string Nombre { get; set; }

    // Propiedad de navegación
    public ICollection<Adquisicion> Adquisiciones { get; set; }
} 