namespace Adres.Application.DTOs;

public class FiltroAdquisicionDto
{
    public int? UnidadAdministrativaId { get; set; }
    public int? TipoBienServicioId { get; set; }
    public int? ProveedorId { get; set; }
    public string? Estado { get; set; }
    public DateTime? FechaDesde { get; set; }
    public DateTime? FechaHasta { get; set; }
} 