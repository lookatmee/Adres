using Adres.Web.Models;
using Adres.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Adres.Web.Pages.Documentacion;

public class DetailsModel : PageModel
{
    private readonly IApiService _apiService;

    public DetailsModel(IApiService apiService)
    {
        _apiService = apiService;
    }

    public DocumentacionViewModel Documento { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            var documento = await _apiService.GetAsync<DocumentacionViewModel>($"documentacion/{id}");
            if (documento == null)
            {
                return NotFound();
            }

            Documento = documento;
            return Page();
        }
        catch (Exception ex)
        {
            TempData["Error"] = $"Error al cargar el documento: {ex.Message}";
            return RedirectToPage("./Index");
        }
    }
} 