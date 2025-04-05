using Adres.Web.Models;
using Adres.Web.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Adres.Web.Pages.Historial;

public class IndexModel : PageModel
{
    private readonly IApiService _apiService;

    public IndexModel(IApiService apiService)
    {
        _apiService = apiService;
    }

    public List<HistorialCambioDto> HistorialCambios { get; set; } = new();

    public async Task OnGetAsync()
    {
        try
        {
            HistorialCambios = await _apiService.GetListAsync<HistorialCambioDto>("historial") ?? new();
        }
        catch (Exception ex)
        {
            TempData["Error"] = $"Error al cargar el historial: {ex.Message}";
        }
    }
} 