using Adres.Web.Models;
using Adres.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Adres.Web.Pages.Documentacion;

public class EditModel : PageModel
{
    private readonly IApiService _apiService;

    public EditModel(IApiService apiService)
    {
        _apiService = apiService;
    }

    [BindProperty]
    public DocumentacionDto Documento { get; set; } = new();

    public SelectList Adquisiciones { get; set; } = default!;

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

            var adquisiciones = await _apiService.GetListAsync<AdquisicionDto>("adquisiciones") ?? new();
            Adquisiciones = new SelectList(adquisiciones, "Id", "Id");

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
        if (!ModelState.IsValid)
        {
            await LoadAdquisiciones();
            return Page();
        }

        try
        {
            await _apiService.PutAsync<DocumentacionDto>($"documentacion/{Documento.Id}", Documento);
            TempData["Success"] = "Documento actualizado exitosamente";
            return RedirectToPage("./Index");
        }
        catch (Exception ex)
        {
            await LoadAdquisiciones();
            ModelState.AddModelError(string.Empty, $"Error al actualizar el documento: {ex.Message}");
            return Page();
        }
    }

    private async Task LoadAdquisiciones()
    {
        var adquisiciones = await _apiService.GetListAsync<AdquisicionDto>("adquisiciones") ?? new();
        Adquisiciones = new SelectList(adquisiciones, "Id", "Id");
    }
} 