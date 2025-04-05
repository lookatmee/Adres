using Microsoft.AspNetCore.Mvc.RazorPages;
using Adres.Web.Models;
using Adres.Web.Services;

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
} 