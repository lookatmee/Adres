namespace Adres.Application.DTOs;

public class DocumentacionDto
{
    public int Id { get; set; }
    public int AdquisicionId { get; set; }
    public string TipoDocumento { get; set; }
    public string NumeroDocumento { get; set; }
    public string Detalle { get; set; }
}

public class CreateDocumentacionDto
{
    public int AdquisicionId { get; set; }
    public string TipoDocumento { get; set; }
    public string NumeroDocumento { get; set; }
    public string Detalle { get; set; }
} 