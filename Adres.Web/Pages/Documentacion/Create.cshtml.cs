using Adres.Web.Models;
using Adres.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Adres.Web.Pages.Documentacion;

public class CreateModel : PageModel
{
    private readonly IApiService _apiService;

    public CreateModel(IApiService apiService)
    {
        _apiService = apiService;
    }

    [BindProperty]
    public DocumentacionDto Documento { get; set; } = new();

    public SelectList Adquisiciones { get; set; } = default!;

    public async Task OnGetAsync()
    {
        try
        {
            var adquisiciones = await _apiService.GetListAsync<AdquisicionDto>("adquisiciones") ?? new();
            Adquisiciones = new SelectList(adquisiciones, "Id", "Id");
        }
        catch (Exception ex)
        {
            TempData["Error"] = $"Error al cargar las adquisiciones: {ex.Message}";
        }
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            await OnGetAsync();
            return Page();
        }

        try
        {
            await _apiService.PostAsync<DocumentacionDto>("documentacion", Documento);
            TempData["Success"] = "Documento creado exitosamente";
            return RedirectToPage("./Index");
        }
        catch (Exception ex)
        {
            await OnGetAsync();
            ModelState.AddModelError(string.Empty, $"Error al crear el documento: {ex.Message}");
            return Page();
        }
    }
} 