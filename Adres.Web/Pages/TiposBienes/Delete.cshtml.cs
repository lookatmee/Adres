using Adres.Web.Models;
using Adres.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Adres.Web.Pages.TiposBienes;

public class DeleteModel : PageModel
{
    private readonly IApiService _apiService;

    public DeleteModel(IApiService apiService)
    {
        _apiService = apiService;
    }

    [BindProperty]
    public TipoBienServicioDto TipoBien { get; set; } = new();

    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            var tipoBien = await _apiService.GetAsync<TipoBienServicioDto>($"TipoBienServicio/{id}");
            if (tipoBien == null)
            {
                return NotFound();
            }

            TipoBien = tipoBien;
            return Page();
        }
        catch (Exception ex)
        {
            TempData["Error"] = $"Error al cargar el tipo de bien/servicio: {ex.Message}";
            return RedirectToPage("./Index");
        }
    }

    public async Task<IActionResult> OnPostAsync()
    {
        try
        {
            await _apiService.DeleteAsync($"TipoBienServicio/{TipoBien.Id}");
            TempData["Success"] = "Tipo de bien/servicio eliminado exitosamente.";
            return RedirectToPage("./Index");
        }
        catch (Exception ex)
        {
            TempData["Error"] = $"Error al eliminar el tipo de bien/servicio: {ex.Message}";
            return RedirectToPage("./Index");
        }
    }
} 