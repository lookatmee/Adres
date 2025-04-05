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

    [BindProperty(SupportsGet = true)]
    public FiltroAdquisicion Filtro { get; set; } = new();

    public List<AdquisicionDto> Adquisiciones { get; set; } = new();
    public SelectList UnidadesLista { get; set; } = null!;
    public SelectList ProveedoresLista { get; set; } = null!;
    public SelectList TiposLista { get; set; } = null!;

    public async Task OnGetAsync()
    {
        try
        {
            // Cargar listas para los filtros
            var unidades = await _apiService.GetListAsync<UnidadAdministrativaDto>("unidades") ?? new List<UnidadAdministrativaDto>();
            var proveedores = await _apiService.GetListAsync<ProveedorDto>("proveedores") ?? new List<ProveedorDto>();
            var tipos = await _apiService.GetListAsync<TipoBienServicioDto>("tiposbienes") ?? new List<TipoBienServicioDto>();

            UnidadesLista = new SelectList(unidades, "Id", "Nombre");
            ProveedoresLista = new SelectList(proveedores, "Id", "Nombre");
            TiposLista = new SelectList(tipos, "Id", "Descripcion");

            // Construir la URL con los filtros
            var queryString = BuildQueryString();
            
            // Obtener adquisiciones filtradas
            Adquisiciones = await _apiService.GetListAsync<AdquisicionDto>($"adquisiciones{queryString}") ?? new List<AdquisicionDto>();
        }
        catch (Exception ex)
        {
            TempData["Error"] = $"Error al cargar las adquisiciones: {ex.Message}";
        }
    }

    private string BuildQueryString()
    {
        var queryParams = new List<string>();

        if (Filtro.UnidadId.HasValue)
            queryParams.Add($"unidadId={Filtro.UnidadId}");
        
        if (Filtro.ProveedorId.HasValue)
            queryParams.Add($"proveedorId={Filtro.ProveedorId}");
        
        if (Filtro.TipoId.HasValue)
            queryParams.Add($"tipoId={Filtro.TipoId}");
        
        if (!string.IsNullOrEmpty(Filtro.Estado))
            queryParams.Add($"estado={Filtro.Estado}");
        
        if (Filtro.FechaDesde.HasValue)
            queryParams.Add($"fechaDesde={Filtro.FechaDesde:yyyy-MM-dd}");
        
        if (Filtro.FechaHasta.HasValue)
            queryParams.Add($"fechaHasta={Filtro.FechaHasta:yyyy-MM-dd}");

        return queryParams.Any() ? "?" + string.Join("&", queryParams) : string.Empty;
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