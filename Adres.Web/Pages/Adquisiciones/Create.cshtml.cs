using Adres.Web.Models;
using Adres.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Adres.Web.Pages.Adquisiciones;

public class CreateModel : PageModel
{
    private readonly IApiService _apiService;

    public CreateModel(IApiService apiService)
    {
        _apiService = apiService;
    }

    [BindProperty]
    public AdquisicionDto Adquisicion { get; set; } = new();

    public SelectList UnidadesAdministrativas { get; set; } = default!;
    public SelectList TiposBienesServicios { get; set; } = default!;
    public SelectList Proveedores { get; set; } = default!;

    public async Task OnGetAsync()
    {
        try
        {
            var unidades = await _apiService.GetListAsync<UnidadAdministrativaDto>("unidadadministrativa") ?? new();
            var tipos = await _apiService.GetListAsync<TipoBienServicioDto>("tipobienservicio") ?? new();
            var proveedores = await _apiService.GetListAsync<ProveedorDto>("proveedor") ?? new();

            UnidadesAdministrativas = new SelectList(unidades, "Id", "Nombre");
            TiposBienesServicios = new SelectList(tipos, "Id", "Descripcion");
            Proveedores = new SelectList(proveedores, "Id", "Nombre");
        }
        catch (Exception ex)
        {
            TempData["Error"] = $"Error al cargar los datos: {ex.Message}";
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
            // Calcular el valor total antes de enviar
            Adquisicion.ValorTotal = Adquisicion.Cantidad * Adquisicion.ValorUnitario;
            
            await _apiService.PostAsync<AdquisicionDto>("adquisiciones", Adquisicion);
            TempData["Success"] = "Adquisición creada exitosamente";
            return RedirectToPage("./Index");
        }
        catch (Exception ex)
        {
            await OnGetAsync();
            ModelState.AddModelError(string.Empty, $"Error al crear la adquisición: {ex.Message}");
            return Page();
        }
    }
} 