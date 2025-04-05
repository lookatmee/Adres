namespace Adres.Domain.Entities;

public class Documentacion
{
    public int Id { get; set; }
    public int AdquisicionId { get; set; }
    public string TipoDocumento { get; set; }
    public string NumeroDocumento { get; set; }
    public string Detalle { get; set; }

    // Propiedad de navegación
    public Adquisicion Adquisicion { get; set; }
} 