using Adres.Web.Models;
using Adres.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Adres.Web.Pages.TiposBienes;

public class CreateModel : PageModel
{
    private readonly IApiService _apiService;

    public CreateModel(IApiService apiService)
    {
        _apiService = apiService;
    }

    [BindProperty]
    public TipoBienServicioDto TipoBien { get; set; } = new();

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

            await _apiService.PostAsync<TipoBienServicioDto>("TipoBienServicio", TipoBien);
            TempData["Success"] = "Tipo de bien/servicio creado exitosamente.";
            return RedirectToPage("./Index");
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, $"Error al crear el tipo de bien/servicio: {ex.Message}");
            return Page();
        }
    }
} 