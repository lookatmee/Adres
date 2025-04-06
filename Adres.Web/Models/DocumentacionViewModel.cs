using System.ComponentModel.DataAnnotations;

namespace Adres.Web.Models;

public class DocumentacionViewModel
{
    public int Id { get; set; }

    [Display(Name = "Adquisición")]
    public int AdquisicionId { get; set; }

    [Display(Name = "Tipo de Documento")]
    public string TipoDocumento { get; set; } = string.Empty;

    [Display(Name = "Número de Documento")]
    public string NumeroDocumento { get; set; } = string.Empty;

    [Display(Name = "Fecha del Documento")]
    [DataType(DataType.Date)]
    public DateTime FechaDocumento { get; set; }

    [Display(Name = "Observaciones")]
    public string? Observaciones { get; set; }

    // Propiedades de navegación
    public AdquisicionViewModel? Adquisicion { get; set; }

    // Propiedades de auditoría
    [Display(Name = "Fecha de Creación")]
    public DateTime FechaCreacion { get; set; }

    [Display(Name = "Creado Por")]
    public string CreadoPor { get; set; } = string.Empty;

    [Display(Name = "Fecha de Modificación")]
    public DateTime? FechaModificacion { get; set; }

    [Display(Name = "Modificado Por")]
    public string? ModificadoPor { get; set; }
} 