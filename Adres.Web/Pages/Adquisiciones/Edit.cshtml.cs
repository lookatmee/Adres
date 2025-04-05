using Adres.Web.Models;
using Adres.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.Json;

namespace Adres.Web.Pages.Adquisiciones;

public class EditModel : PageModel
{
    private readonly IApiService _apiService;

    public EditModel(IApiService apiService)
    {
        _apiService = apiService;
    }

    [BindProperty]
    public UpdateAdquisicionDto Adquisicion { get; set; }

    public SelectList UnidadesAdministrativas { get; set; }
    public SelectList TiposBienesServicios { get; set; }
    public SelectList Proveedores { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        try 
        {
            await CargarListasSeleccion();

            var adquisicion = await _apiService.GetAsync<AdquisicionDto>($"adquisiciones/{id}");
            if (adquisicion == null)
            {
                return NotFound();
            }

            Adquisicion = new UpdateAdquisicionDto
            {
                Id = adquisicion.Id,
                UnidadAdministrativaId = adquisicion.UnidadAdministrativaId,
                TipoBienServicioId = adquisicion.TipoBienServicioId,
                ProveedorId = adquisicion.ProveedorId,
                Cantidad = adquisicion.Cantidad,
                ValorUnitario = adquisicion.ValorUnitario,
                FechaAdquisicion = adquisicion.FechaAdquisicion,
                Estado = adquisicion.Estado
            };

            return Page();
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", $"Error al cargar la adquisición: {ex.Message}");
            return Page();
        }
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            await CargarListasSeleccion();
            return Page();
        }

        try
        {
            await _apiService.PutAsync<UpdateAdquisicionDto>($"adquisiciones/{Adquisicion.Id}", Adquisicion);
            return RedirectToPage("./Index");
        }
        catch (Exception ex)
        {
            // Extraer el mensaje más relevante
            var mensaje = ex.InnerException?.Message ?? ex.Message;
            ModelState.AddModelError("", $"Error al actualizar la adquisición: {mensaje}");
            await CargarListasSeleccion();
            return Page();
        }
    }

    private async Task CargarListasSeleccion()
    {
        try
        {
            var unidades = await _apiService.GetAsync<IEnumerable<UnidadAdministrativaDto>>("unidadadministrativa");
            UnidadesAdministrativas = new SelectList(unidades, "Id", "Nombre");

            var tipos = await _apiService.GetAsync<IEnumerable<TipoBienServicioDto>>("tipobienservicio");
            TiposBienesServicios = new SelectList(tipos, "Id", "Descripcion");

            var proveedores = await _apiService.GetAsync<IEnumerable<ProveedorDto>>("proveedor");
            Proveedores = new SelectList(proveedores, "Id", "Nombre");
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", $"Error al cargar las listas: {ex.Message}");
        }
    }
} 