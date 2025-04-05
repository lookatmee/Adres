namespace Adres.Application.DTOs;

public class AdquisicionDto
{
    public int Id { get; set; }
    public int UnidadAdministrativaId { get; set; }
    public UnidadAdministrativaDto UnidadAdministrativa { get; set; }
    public int TipoBienServicioId { get; set; }
    public TipoBienServicioDto TipoBienServicio { get; set; }
    public int ProveedorId { get; set; }
    public ProveedorDto Proveedor { get; set; }
    public int Cantidad { get; set; }
    public decimal ValorUnitario { get; set; }
    public decimal ValorTotal { get; set; }
    public string Estado { get; set; } = "activo";
    public DateTime FechaAdquisicion { get; set; }
}

public class UnidadAdministrativaDto
{
    public int Id { get; set; }
    public string Nombre { get; set; }
}

public class TipoBienServicioDto
{
    public int Id { get; set; }
    public string Descripcion { get; set; }
}

public class ProveedorDto
{
    public int Id { get; set; }
    public string Nombre { get; set; }
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