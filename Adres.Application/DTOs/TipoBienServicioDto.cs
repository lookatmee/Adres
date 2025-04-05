namespace Adres.Application.DTOs;

public class TipoBienServicioDto
{
    public int Id { get; set; }
    public string Descripcion { get; set; }
}

public class CreateTipoBienServicioDto
{
    public string Descripcion { get; set; }
}

public class UpdateTipoBienServicioDto
{
    public string Descripcion { get; set; }
} 