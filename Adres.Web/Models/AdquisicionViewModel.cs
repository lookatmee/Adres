using System.ComponentModel.DataAnnotations;

namespace Adres.Web.Models;

public class AdquisicionViewModel
{
    public int Id { get; set; }

    [Display(Name = "Unidad Administrativa")]
    public int UnidadAdministrativaId { get; set; }

    [Display(Name = "Tipo de Bien/Servicio")]
    public int TipoBienServicioId { get; set; }

    [Display(Name = "Proveedor")]
    public int ProveedorId { get; set; }

    [Display(Name = "Cantidad")]
    public int Cantidad { get; set; }

    [Display(Name = "Valor Unitario")]
    [DataType(DataType.Currency)]
    public decimal ValorUnitario { get; set; }

    [Display(Name = "Valor Total")]
    [DataType(DataType.Currency)]
    public decimal ValorTotal { get; set; }

    [Display(Name = "Presupuesto")]
    [DataType(DataType.Currency)]
    public decimal Presupuesto { get; set; }

    [Display(Name = "Estado")]
    public string Estado { get; set; } = string.Empty;

    // Propiedades de navegación
    public UnidadAdministrativaViewModel? UnidadAdministrativa { get; set; }
    public TipoBienServicioViewModel? TipoBienServicio { get; set; }
    public ProveedorViewModel? Proveedor { get; set; }
} 