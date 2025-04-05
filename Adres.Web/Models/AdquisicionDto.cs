using System.ComponentModel.DataAnnotations;

namespace Adres.Web.Models;

public class AdquisicionDto
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El presupuesto es requerido")]
    [Display(Name = "Presupuesto")]
    [Range(0, double.MaxValue, ErrorMessage = "El presupuesto debe ser mayor a 0")]
    public decimal Presupuesto { get; set; }

    [Required(ErrorMessage = "La unidad administrativa es requerida")]
    [Display(Name = "Unidad Administrativa")]
    public int UnidadAdministrativaId { get; set; }
    public UnidadAdministrativaDto? UnidadAdministrativa { get; set; }

    [Required(ErrorMessage = "El tipo de bien/servicio es requerido")]
    [Display(Name = "Tipo de Bien/Servicio")]
    public int TipoBienServicioId { get; set; }
    public TipoBienServicioDto? TipoBienServicio { get; set; }

    [Required(ErrorMessage = "El proveedor es requerido")]
    [Display(Name = "Proveedor")]
    public int ProveedorId { get; set; }
    public ProveedorDto? Proveedor { get; set; }

    [Required(ErrorMessage = "La cantidad es requerida")]
    [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor a 0")]
    [Display(Name = "Cantidad")]
    public int Cantidad { get; set; }

    [Required(ErrorMessage = "El valor unitario es requerido")]
    [Range(1, double.MaxValue, ErrorMessage = "El valor unitario debe ser mayor a 0")]
    [Display(Name = "Valor Unitario")]
    public decimal ValorUnitario { get; set; }

    [Display(Name = "Valor Total")]
    public decimal ValorTotal { get; set; }

    [Display(Name = "Estado")]
    public string Estado { get; set; } = "activo";

    [Required(ErrorMessage = "La fecha de adquisición es requerida")]
    [Display(Name = "Fecha de Adquisición")]
    [DataType(DataType.Date)]
    public DateTime FechaAdquisicion { get; set; } = DateTime.Today;

    public string UnidadAdministrativaNombre { get; set; } = string.Empty;
    public string TipoBienServicioDescripcion { get; set; } = string.Empty;
    public List<DocumentacionDto> Documentaciones { get; set; } = new();
    public List<HistorialCambioDto> HistorialCambios { get; set; } = new();
}

public class DocumentacionDto
{
    public int Id { get; set; }
    public int AdquisicionId { get; set; }
    public string TipoDocumento { get; set; } = string.Empty;
    public string NumeroDocumento { get; set; } = string.Empty;
    public string Detalle { get; set; } = string.Empty;
}

public class HistorialCambioDto
{
    public int Id { get; set; }
    public int AdquisicionId { get; set; }
    public DateTime FechaCambio { get; set; }
    public string CampoModificado { get; set; } = string.Empty;
    public string ValorAnterior { get; set; } = string.Empty;
    public string ValorNuevo { get; set; } = string.Empty;
    public string Usuario { get; set; } = string.Empty;
}

public class UnidadAdministrativaDto
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
}

public class TipoBienServicioDto
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
}

public class ProveedorDto
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
} 