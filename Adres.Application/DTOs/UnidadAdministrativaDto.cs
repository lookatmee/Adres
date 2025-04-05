namespace Adres.Application.DTOs;

public class UnidadAdministrativaDto
{
    public int Id { get; set; }
    public string Nombre { get; set; }
}

public class CreateUnidadAdministrativaDto
{
    public string Nombre { get; set; }
}

public class UpdateUnidadAdministrativaDto
{
    public string Nombre { get; set; }
} 