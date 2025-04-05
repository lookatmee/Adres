using Adres.Web.Models;
using Adres.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Adres.Web.Pages.Unidades;

public class CreateModel : PageModel
{
    private readonly IApiService _apiService;

    public CreateModel(IApiService apiService)
    {
        _apiService = apiService;
    }

    [BindProperty]
    public UnidadAdministrativaDto Unidad { get; set; } = new();

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync()
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _apiService.PostAsync<UnidadAdministrativaDto>("UnidadAdministrativa", Unidad);
            TempData["Success"] = "Unidad administrativa creada exitosamente.";
            return RedirectToPage("./Index");
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, $"Error al crear la unidad administrativa: {ex.Message}");
            return Page();
        }
    }
} 