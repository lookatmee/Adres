using Adres.Web.Models;
using Adres.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Adres.Web.Pages.Documentacion;

public class DeleteModel : PageModel
{
    private readonly IApiService _apiService;

    public DeleteModel(IApiService apiService)
    {
        _apiService = apiService;
    }

    [BindProperty]
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

    public async Task<IActionResult> OnPostDeleteAsync(int id)
    {
        try
        {
            var success = await _apiService.DeleteAsync($"documentacion/{id}");
            if (!success)
            {
                throw new Exception("No se pudo eliminar el documento");
            }

            TempData["Success"] = "Documento eliminado correctamente";
            return RedirectToPage("./Index");
        }
        catch (Exception ex)
        {
            TempData["Error"] = $"Error al eliminar el documento: {ex.Message}";
            return Page();
        }
    }
} 