namespace Adres.Application.DTOs;

public class DocumentacionDto
{
    public int Id { get; set; }
    public int AdquisicionId { get; set; }
    public string TipoDocumento { get; set; } = string.Empty;
    public string NumeroDocumento { get; set; } = string.Empty;
    public DateTime FechaDocumento { get; set; }
    public string? Observaciones { get; set; }
} 