using Adres.Web.Models;
using Adres.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Adres.Web.Pages.Unidades;

public class EditModel : PageModel
{
    private readonly IApiService _apiService;

    public EditModel(IApiService apiService)
    {
        _apiService = apiService;
    }

    [BindProperty]
    public UnidadAdministrativaDto Unidad { get; set; } = new();

    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            var unidad = await _apiService.GetAsync<UnidadAdministrativaDto>($"UnidadAdministrativa/{id}");
            if (unidad == null)
            {
                return NotFound();
            }

            Unidad = unidad;
            return Page();
        }
        catch (Exception ex)
        {
            TempData["Error"] = $"Error al cargar la unidad administrativa: {ex.Message}";
            return RedirectToPage("./Index");
        }
    }

    public async Task<IActionResult> OnPostAsync()
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _apiService.PutAsync<UnidadAdministrativaDto>($"UnidadAdministrativa/{Unidad.Id}", Unidad);
            TempData["Success"] = "Unidad administrativa actualizada exitosamente.";
            return RedirectToPage("./Index");
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, $"Error al actualizar la unidad administrativa: {ex.Message}");
            return Page();
        }
    }
} 