namespace Adres.Domain.Entities;

public class TipoBienServicio
{
    public int Id { get; set; }
    public string Descripcion { get; set; }

    // Propiedad de navegación
    public ICollection<Adquisicion> Adquisiciones { get; set; }
} 