namespace Adres.Application.DTOs;

public class ProveedorDto
{
    public int Id { get; set; }
    public string Nombre { get; set; }
}

public class CreateProveedorDto
{
    public string Nombre { get; set; }
}

public class UpdateProveedorDto
{
    public string Nombre { get; set; }
} 