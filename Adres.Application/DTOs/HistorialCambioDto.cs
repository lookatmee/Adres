namespace Adres.Application.DTOs;

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