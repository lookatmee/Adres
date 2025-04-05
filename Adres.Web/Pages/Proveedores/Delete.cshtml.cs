using Adres.Web.Models;
using Adres.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Adres.Web.Pages.Proveedores;

public class DeleteModel : PageModel
{
    private readonly IApiService _apiService;

    public DeleteModel(IApiService apiService)
    {
        _apiService = apiService;
    }

    [BindProperty]
    public ProveedorDto Proveedor { get; set; } = new();

    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            var proveedor = await _apiService.GetAsync<ProveedorDto>($"Proveedor/{id}");
            if (proveedor == null)
            {
                return NotFound();
            }

            Proveedor = proveedor;
            return Page();
        }
        catch (Exception ex)
        {
            TempData["Error"] = $"Error al cargar el proveedor: {ex.Message}";
            return RedirectToPage("./Index");
        }
    }

    public async Task<IActionResult> OnPostAsync()
    {
        try
        {
            var result = await _apiService.DeleteAsync($"Proveedor/{Proveedor.Id}");
            if (result)
            {
                TempData["Success"] = "Proveedor eliminado exitosamente";
                return RedirectToPage("./Index");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "No se pudo eliminar el proveedor");
                return Page();
            }
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, $"Error al eliminar el proveedor: {ex.Message}");
            return Page();
        }
    }
} 