namespace Adres.Web.Models;

public class HistorialDto
{
    public int Id { get; set; }
    public int AdquisicionId { get; set; }
    public DateTime Fecha { get; set; }
    public string CampoModificado { get; set; } = string.Empty;
    public string ValorAnterior { get; set; } = string.Empty;
    public string ValorNuevo { get; set; } = string.Empty;
    public string Usuario { get; set; } = string.Empty;
} 