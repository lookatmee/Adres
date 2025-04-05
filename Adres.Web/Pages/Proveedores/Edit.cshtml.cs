using Adres.Web.Models;
using Adres.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Adres.Web.Pages.Proveedores;

public class EditModel : PageModel
{
    private readonly IApiService _apiService;

    public EditModel(IApiService apiService)
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
        if (!ModelState.IsValid)
        {
            return Page();
        }

        try
        {
            await _apiService.PutAsync<ProveedorDto>($"Proveedor/{Proveedor.Id}", Proveedor);
            TempData["Success"] = "Proveedor actualizado exitosamente";
            return RedirectToPage("./Index");
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, $"Error al actualizar el proveedor: {ex.Message}");
            return Page();
        }
    }
} 