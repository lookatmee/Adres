using Adres.Web.Models;
using Adres.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Adres.Web.Pages.Proveedores;

public class CreateModel : PageModel
{
    private readonly IApiService _apiService;

    public CreateModel(IApiService apiService)
    {
        _apiService = apiService;
    }

    [BindProperty]
    public ProveedorDto Proveedor { get; set; } = new();

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        try
        {
            await _apiService.PostAsync<ProveedorDto>("Proveedor", Proveedor);
            TempData["Success"] = "Proveedor creado exitosamente";
            return RedirectToPage("./Index");
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, $"Error al crear el proveedor: {ex.Message}");
            return Page();
        }
    }
} 