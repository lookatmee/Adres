using Adres.Web.Models;
using Adres.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Adres.Web.Pages.Adquisiciones;

public class DeleteModel : PageModel
{
    private readonly IApiService _apiService;

    public DeleteModel(IApiService apiService)
    {
        _apiService = apiService;
    }

    [BindProperty]
    public AdquisicionDto Adquisicion { get; set; } = new();

    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            var adquisicion = await _apiService.GetAsync<AdquisicionDto>($"adquisiciones/{id}");
            if (adquisicion == null)
            {
                return NotFound();
            }

            Adquisicion = adquisicion;
            return Page();
        }
        catch (Exception ex)
        {
            TempData["Error"] = $"Error al cargar la adquisición: {ex.Message}";
            return RedirectToPage("./Index");
        }
    }

    public async Task<IActionResult> OnPostAsync()
    {
        try
        {
            await _apiService.PatchAsync($"adquisiciones/{Adquisicion.Id}/desactivar");
            TempData["Success"] = "Adquisición desactivada exitosamente";
            return RedirectToPage("./Index");
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, $"Error al desactivar la adquisición: {ex.Message}");
            return Page();
        }
    }
} 