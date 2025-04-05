using Adres.Web.Models;
using Adres.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Adres.Web.Pages.Unidades;

public class IndexModel : PageModel
{
    private readonly IApiService _apiService;

    public IndexModel(IApiService apiService)
    {
        _apiService = apiService;
    }

    public List<UnidadAdministrativaDto> Unidades { get; set; } = new();

    public async Task OnGetAsync()
    {
        try
        {
            Unidades = await _apiService.GetListAsync<UnidadAdministrativaDto>("UnidadAdministrativa") ?? new();
        }
        catch (Exception ex)
        {
            TempData["Error"] = $"Error al cargar las unidades administrativas: {ex.Message}";
        }
    }
} 