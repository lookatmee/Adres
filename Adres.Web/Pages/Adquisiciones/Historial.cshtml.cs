using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Adres.Web.Models;
using Adres.Web.Services;

namespace Adres.Web.Pages.Adquisiciones;

public class HistorialModel : PageModel
{
    private readonly IApiService _apiService;

    public HistorialModel(IApiService apiService)
    {
        _apiService = apiService;
    }

    public List<HistorialDto> Historial { get; set; } = new();

    public async Task<IActionResult> OnGetAsync()
    {
        try
        {
            Historial = await _apiService.GetListAsync<HistorialDto>("historial") ?? new();
            return Page();
        }
        catch (Exception ex)
        {
            TempData["Error"] = $"Error al cargar el historial: {ex.Message}";
            return RedirectToPage("./Index");
        }
    }
} 