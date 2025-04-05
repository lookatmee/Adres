using Adres.Web.Models;
using Adres.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Adres.Web.Pages.TiposBienes;

public class IndexModel : PageModel
{
    private readonly IApiService _apiService;

    public IndexModel(IApiService apiService)
    {
        _apiService = apiService;
    }

    public List<TipoBienServicioDto> TiposBienes { get; set; } = new();

    public async Task OnGetAsync()
    {
        try
        {
            TiposBienes = await _apiService.GetListAsync<TipoBienServicioDto>("TipoBienServicio") ?? new();
        }
        catch (Exception ex)
        {
            TempData["Error"] = $"Error al cargar los tipos de bienes/servicios: {ex.Message}";
        }
    }
} 