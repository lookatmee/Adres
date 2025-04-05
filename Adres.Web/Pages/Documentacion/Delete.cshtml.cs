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
    public DocumentacionDto Documento { get; set; } = new();

    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            var documento = await _apiService.GetAsync<DocumentacionDto>($"documentacion/{id}");
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

    public async Task<IActionResult> OnPostAsync()
    {
        try
        {
            var result = await _apiService.DeleteAsync($"documentacion/{Documento.Id}");
            if (result)
            {
                TempData["Success"] = "Documento eliminado exitosamente";
                return RedirectToPage("./Index");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "No se pudo eliminar el documento");
                return Page();
            }
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, $"Error al eliminar el documento: {ex.Message}");
            return Page();
        }
    }
} 