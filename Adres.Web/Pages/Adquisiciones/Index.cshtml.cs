using Adres.Web.Models;
using Adres.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Adres.Web.Pages.Adquisiciones;

public class IndexModel : PageModel
{
    private readonly IApiService _apiService;

    public IndexModel(IApiService apiService)
    {
        _apiService = apiService;
    }

    public IEnumerable<AdquisicionDto> Adquisiciones { get; set; } = new List<AdquisicionDto>();
    public SelectList UnidadesAdministrativas { get; set; }
    public SelectList TiposBienesServicios { get; set; }
    public SelectList Proveedores { get; set; }

    [BindProperty(SupportsGet = true)]
    public int? UnidadAdministrativaId { get; set; }

    [BindProperty(SupportsGet = true)]
    public int? TipoBienServicioId { get; set; }

    [BindProperty(SupportsGet = true)]
    public int? ProveedorId { get; set; }

    [BindProperty(SupportsGet = true)]
    public string? Estado { get; set; }

    [BindProperty(SupportsGet = true)]
    public DateTime? FechaDesde { get; set; }

    [BindProperty(SupportsGet = true)]
    public DateTime? FechaHasta { get; set; }

    public async Task OnGetAsync()
    {
        await CargarListasSeleccion();
        await CargarAdquisiciones();
    }

    private async Task CargarListasSeleccion()
    {
        var unidades = await _apiService.GetAsync<IEnumerable<UnidadAdministrativaDto>>("unidadadministrativa");
        UnidadesAdministrativas = new SelectList(unidades, "Id", "Nombre");

        var tipos = await _apiService.GetAsync<IEnumerable<TipoBienServicioDto>>("tipobienservicio");
        TiposBienesServicios = new SelectList(tipos, "Id", "Descripcion");

        var proveedores = await _apiService.GetAsync<IEnumerable<ProveedorDto>>("proveedor");
        Proveedores = new SelectList(proveedores, "Id", "Nombre");
    }

    private async Task CargarAdquisiciones()
    {
        var queryParams = new List<string>();

        if (UnidadAdministrativaId.HasValue)
            queryParams.Add($"UnidadAdministrativaId={UnidadAdministrativaId}");
        if (TipoBienServicioId.HasValue)
            queryParams.Add($"TipoBienServicioId={TipoBienServicioId}");
        if (ProveedorId.HasValue)
            queryParams.Add($"ProveedorId={ProveedorId}");
        if (!string.IsNullOrEmpty(Estado))
            queryParams.Add($"Estado={Estado}");
        if (FechaDesde.HasValue)
            queryParams.Add($"FechaDesde={FechaDesde:yyyy-MM-dd}");
        if (FechaHasta.HasValue)
            queryParams.Add($"FechaHasta={FechaHasta:yyyy-MM-dd}");

        var endpoint = queryParams.Any() 
            ? $"adquisiciones/buscar?{string.Join("&", queryParams)}"
            : "adquisiciones";

        Adquisiciones = await _apiService.GetAsync<IEnumerable<AdquisicionDto>>(endpoint);
    }
}

public class FiltroAdquisicion
{
    public int? UnidadId { get; set; }
    public int? ProveedorId { get; set; }
    public int? TipoId { get; set; }
    public string? Estado { get; set; }
    public DateTime? FechaDesde { get; set; }
    public DateTime? FechaHasta { get; set; }
} 