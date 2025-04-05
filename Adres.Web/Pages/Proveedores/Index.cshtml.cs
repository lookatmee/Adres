using Adres.Web.Models;
using Adres.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Adres.Web.Pages.Proveedores;

public class IndexModel : PageModel
{
    private readonly IApiService _apiService;

    public IndexModel(IApiService apiService)
    {
        _apiService = apiService;
    }

    public List<ProveedorDto> Proveedores { get; set; } = new();

    public async Task OnGetAsync()
    {
        try
        {
            Proveedores = await _apiService.GetListAsync<ProveedorDto>("Proveedor") ?? new();
        }
        catch (Exception ex)
        {
            TempData["Error"] = $"Error al cargar los proveedores: {ex.Message}";
        }
    }
} 