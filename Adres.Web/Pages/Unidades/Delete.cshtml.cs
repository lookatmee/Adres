using Adres.Web.Models;
using Adres.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Adres.Web.Pages.Unidades;

public class DeleteModel : PageModel
{
    private readonly IApiService _apiService;

    public DeleteModel(IApiService apiService)
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
            await _apiService.DeleteAsync($"UnidadAdministrativa/{Unidad.Id}");
            TempData["Success"] = "Unidad administrativa eliminada exitosamente.";
            return RedirectToPage("./Index");
        }
        catch (Exception ex)
        {
            TempData["Error"] = $"Error al eliminar la unidad administrativa: {ex.Message}";
            return RedirectToPage("./Index");
        }
    }
} 