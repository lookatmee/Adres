using System.ComponentModel.DataAnnotations;

namespace Adres.Web.Models;

public class DocumentacionWebDto
{
    public int Id { get; set; }

    [Required(ErrorMessage = "La adquisición es requerida")]
    [Display(Name = "Adquisición")]
    public int AdquisicionId { get; set; }

    [Required(ErrorMessage = "El tipo de documento es requerido")]
    [Display(Name = "Tipo de Documento")]
    public string TipoDocumento { get; set; } = string.Empty;

    [Required(ErrorMessage = "El número de documento es requerido")]
    [Display(Name = "Número de Documento")]
    public string NumeroDocumento { get; set; } = string.Empty;

    [Required(ErrorMessage = "La fecha del documento es requerida")]
    [Display(Name = "Fecha del Documento")]
    [DataType(DataType.Date)]
    public DateTime FechaDocumento { get; set; } = DateTime.Today;

    [Display(Name = "Observaciones")]
    public string? Observaciones { get; set; }

    public AdquisicionDto? Adquisicion { get; set; }
} 