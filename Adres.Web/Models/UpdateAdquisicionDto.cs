using System.ComponentModel.DataAnnotations;

namespace Adres.Web.Models;

public class UpdateAdquisicionDto
{
    public int Id { get; set; }

    [Required(ErrorMessage = "La Unidad Administrativa es requerida")]
    [Display(Name = "Unidad Administrativa")]
    public int UnidadAdministrativaId { get; set; }

    [Required(ErrorMessage = "El Tipo de Bien/Servicio es requerido")]
    [Display(Name = "Tipo de Bien/Servicio")]
    public int TipoBienServicioId { get; set; }

    [Required(ErrorMessage = "El Proveedor es requerido")]
    [Display(Name = "Proveedor")]
    public int ProveedorId { get; set; }

    [Required(ErrorMessage = "La Cantidad es requerida")]
    [Range(1, int.MaxValue, ErrorMessage = "La Cantidad debe ser mayor a 0")]
    public int Cantidad { get; set; }

    [Required(ErrorMessage = "El Valor Unitario es requerido")]
    [Range(1, double.MaxValue, ErrorMessage = "El Valor Unitario debe ser mayor a 0")]
    [Display(Name = "Valor Unitario")]
    public decimal ValorUnitario { get; set; }

    [Required(ErrorMessage = "La Fecha de Adquisición es requerida")]
    [Display(Name = "Fecha de Adquisición")]
    [DataType(DataType.Date)]
    public DateTime FechaAdquisicion { get; set; }

    [Required(ErrorMessage = "El Estado es requerido")]
    [Display(Name = "Estado")]
    public string Estado { get; set; }
} 