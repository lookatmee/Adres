using System.ComponentModel.DataAnnotations;

namespace Adres.Application.DTOs;

public class CreateDocumentacionDto
{
    [Required(ErrorMessage = "La adquisición es requerida")]
    public int AdquisicionId { get; set; }

    [Required(ErrorMessage = "El tipo de documento es requerido")]
    [StringLength(100, ErrorMessage = "El tipo de documento no puede exceder los 100 caracteres")]
    public string TipoDocumento { get; set; } = string.Empty;

    [Required(ErrorMessage = "El número de documento es requerido")]
    [StringLength(50, ErrorMessage = "El número de documento no puede exceder los 50 caracteres")]
    public string NumeroDocumento { get; set; } = string.Empty;

    [Required(ErrorMessage = "La fecha del documento es requerida")]
    public DateTime FechaDocumento { get; set; }

    [StringLength(500, ErrorMessage = "Las observaciones no pueden exceder los 500 caracteres")]
    public string? Observaciones { get; set; }
} 