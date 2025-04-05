namespace Adres.Domain.Entities;

public class HistorialCambios
{
    public int Id { get; set; }
    public int AdquisicionId { get; set; }
    public DateTime FechaCambio { get; set; }
    public string CampoModificado { get; set; }
    public string ValorAnterior { get; set; }
    public string ValorNuevo { get; set; }
    public string Usuario { get; set; }

    // Propiedad de navegación
    public Adquisicion Adquisicion { get; set; }
} 