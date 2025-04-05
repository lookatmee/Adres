namespace Adres.Domain.Entities;

public class Adquisicion
{
    public int Id { get; set; }
    public decimal Presupuesto { get; set; }
    public int UnidadAdministrativaId { get; set; }
    public int TipoBienServicioId { get; set; }
    public int Cantidad { get; set; }
    public decimal ValorUnitario { get; set; }
    public decimal ValorTotal { get; set; }
    public DateTime FechaAdquisicion { get; set; }
    public int ProveedorId { get; set; }
    public string Estado { get; set; } = "activo";

    // Propiedades de navegación
    public UnidadAdministrativa UnidadAdministrativa { get; set; }
    public TipoBienServicio TipoBienServicio { get; set; }
    public Proveedor Proveedor { get; set; }
    public ICollection<Documentacion> Documentaciones { get; set; }
    public ICollection<HistorialCambios> HistorialCambios { get; set; }
} 