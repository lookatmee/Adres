namespace Adres.Application.DTOs;

public class AdquisicionDto
{
    public int Id { get; set; }
    public decimal Presupuesto { get; set; }
    public int UnidadAdministrativaId { get; set; }
    public string UnidadAdministrativaNombre { get; set; }
    public int TipoBienServicioId { get; set; }
    public string TipoBienServicioDescripcion { get; set; }
    public int Cantidad { get; set; }
    public decimal ValorUnitario { get; set; }
    public decimal ValorTotal { get; set; }
    public DateTime FechaAdquisicion { get; set; }
    public int ProveedorId { get; set; }
    public string ProveedorNombre { get; set; }
    public string Estado { get; set; }
}

public class CreateAdquisicionDto
{
    public decimal Presupuesto { get; set; }
    public int UnidadAdministrativaId { get; set; }
    public int TipoBienServicioId { get; set; }
    public int Cantidad { get; set; }
    public decimal ValorUnitario { get; set; }
    public int ProveedorId { get; set; }
}

public class UpdateAdquisicionDto
{
    public decimal Presupuesto { get; set; }
    public int UnidadAdministrativaId { get; set; }
    public int TipoBienServicioId { get; set; }
    public int Cantidad { get; set; }
    public decimal ValorUnitario { get; set; }
    public int ProveedorId { get; set; }
} 