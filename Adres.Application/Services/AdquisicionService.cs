using AutoMapper;
using Adres.Domain.Entities;
using Adres.Application.DTOs;
using Adres.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace Adres.Application.Services;

public interface IAdquisicionService
{
    Task<AdquisicionDto> CreateAsync(CreateAdquisicionDto createDto, string usuario);
    Task<AdquisicionDto> GetByIdAsync(int id);
    Task<IEnumerable<AdquisicionDto>> GetAllAsync();
    Task<IEnumerable<AdquisicionDto>> BuscarAsync(FiltroAdquisicionDto filtro);
    Task UpdateAsync(int id, UpdateAdquisicionDto updateDto, string usuario);
    Task DesactivarAsync(int id, string usuario);
}

public class AdquisicionService : IAdquisicionService
{
    private readonly IRepository<Adquisicion> _adquisicionRepository;
    private readonly IRepository<HistorialCambios> _historialRepository;
    private readonly IMapper _mapper;

    public AdquisicionService(
        IRepository<Adquisicion> adquisicionRepository,
        IRepository<HistorialCambios> historialRepository,
        IMapper mapper)
    {
        _adquisicionRepository = adquisicionRepository;
        _historialRepository = historialRepository;
        _mapper = mapper;
    }

    public async Task<AdquisicionDto> CreateAsync(CreateAdquisicionDto createDto, string usuario)
    {
        var adquisicion = _mapper.Map<Adquisicion>(createDto);
        adquisicion.ValorTotal = createDto.Cantidad * createDto.ValorUnitario;
        adquisicion.FechaAdquisicion = DateTime.Now;
        adquisicion.Estado = "Activo";

        var result = await _adquisicionRepository.AddAsync(adquisicion);
        
        await RegistrarHistorial(result.Id, "Creación", "", "Nueva adquisición", usuario);

        // Cargar las relaciones antes de mapear al DTO
        result = await _adquisicionRepository.GetQueryable()
            .Include(a => a.UnidadAdministrativa)
            .Include(a => a.TipoBienServicio)
            .Include(a => a.Proveedor)
            .FirstOrDefaultAsync(a => a.Id == result.Id);

        return _mapper.Map<AdquisicionDto>(result);
    }

    public async Task<AdquisicionDto> GetByIdAsync(int id)
    {
        var adquisicion = await _adquisicionRepository.GetQueryable()
            .Include(a => a.UnidadAdministrativa)
            .Include(a => a.TipoBienServicio)
            .Include(a => a.Proveedor)
            .FirstOrDefaultAsync(a => a.Id == id);

        return _mapper.Map<AdquisicionDto>(adquisicion);
    }

    public async Task<IEnumerable<AdquisicionDto>> GetAllAsync()
    {
        var adquisiciones = await _adquisicionRepository.GetQueryable()
            .Include(a => a.UnidadAdministrativa)
            .Include(a => a.TipoBienServicio)
            .Include(a => a.Proveedor)
            .ToListAsync();

        return _mapper.Map<IEnumerable<AdquisicionDto>>(adquisiciones);
    }

    public async Task<IEnumerable<AdquisicionDto>> BuscarAsync(FiltroAdquisicionDto filtro)
    {
        IQueryable<Adquisicion> query = _adquisicionRepository.GetQueryable()
            .Include(a => a.UnidadAdministrativa)
            .Include(a => a.TipoBienServicio)
            .Include(a => a.Proveedor);

        // Aplicar filtros
        if (filtro.UnidadAdministrativaId.HasValue)
            query = query.Where(a => a.UnidadAdministrativaId == filtro.UnidadAdministrativaId);

        if (filtro.TipoBienServicioId.HasValue)
            query = query.Where(a => a.TipoBienServicioId == filtro.TipoBienServicioId);

        if (filtro.ProveedorId.HasValue)
            query = query.Where(a => a.ProveedorId == filtro.ProveedorId);

        if (!string.IsNullOrEmpty(filtro.Estado))
            query = query.Where(a => a.Estado.ToLower() == filtro.Estado.ToLower());

        if (filtro.FechaDesde.HasValue)
            query = query.Where(a => a.FechaAdquisicion.Date >= filtro.FechaDesde.Value.Date);

        if (filtro.FechaHasta.HasValue)
            query = query.Where(a => a.FechaAdquisicion.Date <= filtro.FechaHasta.Value.Date);

        var adquisiciones = await query.ToListAsync();
        return _mapper.Map<IEnumerable<AdquisicionDto>>(adquisiciones);
    }

    public async Task UpdateAsync(int id, UpdateAdquisicionDto updateDto, string usuario)
    {
        var adquisicion = await _adquisicionRepository.GetQueryable()
            .Include(a => a.UnidadAdministrativa)
            .Include(a => a.TipoBienServicio)
            .Include(a => a.Proveedor)
            .FirstOrDefaultAsync(a => a.Id == id);

        if (adquisicion == null)
            throw new Exception($"No se encontró la adquisición con ID {id}");
        
        // Guardar valores anteriores relevantes para el historial
        var valorAnteriorObj = new
        {
            adquisicion.UnidadAdministrativaId,
            adquisicion.TipoBienServicioId,
            adquisicion.ProveedorId,
            adquisicion.Cantidad,
            adquisicion.ValorUnitario,
            adquisicion.ValorTotal,
            adquisicion.Estado,
            adquisicion.FechaAdquisicion
        };
        var valorAnterior = System.Text.Json.JsonSerializer.Serialize(valorAnteriorObj);
        
        // Actualizar propiedades manualmente para asegurar el formato correcto
        adquisicion.UnidadAdministrativaId = updateDto.UnidadAdministrativaId;
        adquisicion.TipoBienServicioId = updateDto.TipoBienServicioId;
        adquisicion.ProveedorId = updateDto.ProveedorId;
        adquisicion.Cantidad = updateDto.Cantidad;
        adquisicion.ValorUnitario = updateDto.ValorUnitario;
        adquisicion.FechaAdquisicion = updateDto.FechaAdquisicion;
        adquisicion.Estado = updateDto.Estado; // Esto mantendrá el formato exacto que viene del DTO
        adquisicion.ValorTotal = updateDto.Cantidad * updateDto.ValorUnitario;
        
        await _adquisicionRepository.UpdateAsync(adquisicion);

        var valorNuevoObj = new
        {
            adquisicion.UnidadAdministrativaId,
            adquisicion.TipoBienServicioId,
            adquisicion.ProveedorId,
            adquisicion.Cantidad,
            adquisicion.ValorUnitario,
            adquisicion.ValorTotal,
            adquisicion.Estado,
            adquisicion.FechaAdquisicion
        };
        
        await RegistrarHistorial(id, "Actualización", 
            valorAnterior, 
            System.Text.Json.JsonSerializer.Serialize(valorNuevoObj), 
            usuario);
    }

    public async Task DesactivarAsync(int id, string usuario)
    {
        var adquisicion = await _adquisicionRepository.GetByIdAsync(id);
        if (adquisicion == null)
            throw new Exception($"No se encontró la adquisición con ID {id}");

        var estadoAnterior = adquisicion.Estado;
        
        adquisicion.Estado = "Inactivo"; // Cambiado a mayúscula inicial
        await _adquisicionRepository.UpdateAsync(adquisicion);
        
        await RegistrarHistorial(id, "Estado", 
            estadoAnterior, 
            adquisicion.Estado, // Usar el mismo valor que se guardó
            usuario);
    }

    private async Task RegistrarHistorial(int adquisicionId, string campo, string valorAnterior, 
        string valorNuevo, string usuario)
    {
        var historial = new HistorialCambios
        {
            AdquisicionId = adquisicionId,
            FechaCambio = DateTime.Now,
            CampoModificado = campo,
            ValorAnterior = valorAnterior,
            ValorNuevo = valorNuevo,
            Usuario = usuario
        };

        await _historialRepository.AddAsync(historial);
    }
} 