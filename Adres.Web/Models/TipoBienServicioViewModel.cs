using System.ComponentModel.DataAnnotations;

namespace Adres.Web.Models;

public class TipoBienServicioViewModel
{
    public int Id { get; set; }

    [Display(Name = "Descripción")]
    public string Descripcion { get; set; } = string.Empty;
} 