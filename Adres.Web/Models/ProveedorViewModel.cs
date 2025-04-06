using System.ComponentModel.DataAnnotations;

namespace Adres.Web.Models;

public class ProveedorViewModel
{
    public int Id { get; set; }

    [Display(Name = "Nombre")]
    public string Nombre { get; set; } = string.Empty;
} 