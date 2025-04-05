using Adres.Web.Models;
using Adres.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Adres.Web.Pages.TiposBienes;

public class EditModel : PageModel
{
    private readonly IApiService _apiService;

    public EditModel(IApiService apiService)
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
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _apiService.PutAsync<TipoBienServicioDto>($"TipoBienServicio/{TipoBien.Id}", TipoBien);
            TempData["Success"] = "Tipo de bien/servicio actualizado exitosamente.";
            return RedirectToPage("./Index");
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, $"Error al actualizar el tipo de bien/servicio: {ex.Message}");
            return Page();
        }
    }
} 