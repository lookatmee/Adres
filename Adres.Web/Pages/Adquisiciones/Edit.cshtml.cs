using Adres.Web.Models;
using Adres.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Adres.Web.Pages.Adquisiciones;

public class EditModel : PageModel
{
    private readonly IApiService _apiService;

    public EditModel(IApiService apiService)
    {
        _apiService = apiService;
    }

    [BindProperty]
    public AdquisicionDto Adquisicion { get; set; } = new();

    public SelectList UnidadesAdministrativas { get; set; } = default!;
    public SelectList TiposBienesServicios { get; set; } = default!;
    public SelectList Proveedores { get; set; } = default!;

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

            var unidades = await _apiService.GetListAsync<UnidadAdministrativaDto>("unidadesadministrativas") ?? new();
            var tipos = await _apiService.GetListAsync<TipoBienServicioDto>("tiposbienes") ?? new();
            var proveedores = await _apiService.GetListAsync<ProveedorDto>("proveedores") ?? new();

            UnidadesAdministrativas = new SelectList(unidades, "Id", "Nombre");
            TiposBienesServicios = new SelectList(tipos, "Id", "Descripcion");
            Proveedores = new SelectList(proveedores, "Id", "Nombre");

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
        if (!ModelState.IsValid)
        {
            await LoadSelectLists();
            return Page();
        }

        try
        {
            await _apiService.PutAsync<AdquisicionDto>($"adquisiciones/{Adquisicion.Id}", Adquisicion);
            TempData["Success"] = "Adquisición actualizada exitosamente";
            return RedirectToPage("./Index");
        }
        catch (Exception ex)
        {
            await LoadSelectLists();
            ModelState.AddModelError(string.Empty, $"Error al actualizar la adquisición: {ex.Message}");
            return Page();
        }
    }

    private async Task LoadSelectLists()
    {
        var unidades = await _apiService.GetListAsync<UnidadAdministrativaDto>("unidadesadministrativas") ?? new();
        var tipos = await _apiService.GetListAsync<TipoBienServicioDto>("tiposbienes") ?? new();
        var proveedores = await _apiService.GetListAsync<ProveedorDto>("proveedores") ?? new();

        UnidadesAdministrativas = new SelectList(unidades, "Id", "Nombre");
        TiposBienesServicios = new SelectList(tipos, "Id", "Descripcion");
        Proveedores = new SelectList(proveedores, "Id", "Nombre");
    }
} 