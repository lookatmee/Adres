using Adres.Domain.Common;

namespace Adres.Domain.Entities;

public class Documentacion : AuditableEntity
{
    public int Id { get; set; }
    public int AdquisicionId { get; set; }
    public string TipoDocumento { get; set; } = string.Empty;
    public string NumeroDocumento { get; set; } = string.Empty;
    public DateTime FechaDocumento { get; set; }
    public string? Observaciones { get; set; }

    // Propiedad de navegación
    public virtual Adquisicion Adquisicion { get; set; } = null!;
} 