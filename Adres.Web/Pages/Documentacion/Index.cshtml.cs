using Adres.Web.Models;
using Adres.Web.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Adres.Web.Pages.Documentacion;

public class IndexModel : PageModel
{
    private readonly IApiService _apiService;

    public IndexModel(IApiService apiService)
    {
        _apiService = apiService;
    }

    public List<DocumentacionDto> Documentos { get; set; } = new();

    public async Task OnGetAsync()
    {
        try
        {
            Documentos = await _apiService.GetListAsync<DocumentacionDto>("documentacion") ?? new();
        }
        catch (Exception ex)
        {
            TempData["Error"] = $"Error al cargar los documentos: {ex.Message}";
        }
    }
} 