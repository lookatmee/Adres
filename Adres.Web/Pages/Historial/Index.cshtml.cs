using Microsoft.AspNetCore.Mvc.RazorPages;
using Adres.Web.Models;
using Adres.Web.Services;
using System.Text;

namespace Adres.Web.Pages.Historial;

public class IndexModel : PageModel
{
    private readonly IApiService _apiService;

    public IndexModel(IApiService apiService)
    {
        _apiService = apiService;
    }

    public IEnumerable<HistorialCambioDto> HistorialCambios { get; set; }

    public async Task OnGetAsync()
    {
        try
        {
            HistorialCambios = await _apiService.GetAsync<IEnumerable<HistorialCambioDto>>("historial");
        }
        catch (Exception ex)
        {
            // Manejar el error apropiadamente
            HistorialCambios = Enumerable.Empty<HistorialCambioDto>();
            ModelState.AddModelError("", "Error al cargar el historial de cambios.");
        }
    }

    public string FormatearJson(string json)
    {
        if (string.IsNullOrEmpty(json)) return json;
        if (json.StartsWith("\"")) json = json.Trim('"');

        var sb = new StringBuilder();
        var propiedades = json.TrimStart('{').TrimEnd('}').Split(',');

        foreach (var prop in propiedades)
        {
            var partes = prop.Split(':');
            if (partes.Length == 2)
            {
                var nombre = partes[0].Trim().Trim('"');
                var valor = partes[1].Trim().Trim('"');

                // Formatear el nombre de la propiedad
                var nombreFormateado = nombre switch
                {
                    "UnidadAdministrativaId" => "Unidad Administrativa",
                    "TipoBienServicioId" => "Tipo de Bien/Servicio",
                    "ProveedorId" => "Proveedor",
                    "Cantidad" => "Cantidad",
                    "ValorUnitario" => "Valor Unitario",
                    "ValorTotal" => "Valor Total",
                    "FechaAdquisicion" => "Fecha de Adquisición",
                    "Estado" => "Estado",
                    _ => nombre
                };

                // Formatear el valor
                if (valor.Contains("T00:00:00"))
                {
                    if (DateTime.TryParse(valor, out DateTime fecha))
                    {
                        valor = fecha.ToString("dd/MM/yyyy");
                    }
                }
                else if (decimal.TryParse(valor, out decimal numero))
                {
                    valor = numero.ToString("C");
                }

                sb.AppendLine($"{nombreFormateado}: {valor}");
            }
        }

        return sb.ToString();
    }
} 