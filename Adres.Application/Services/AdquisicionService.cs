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
        adquisicion.Estado = "activo";

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

    public async Task UpdateAsync(int id, UpdateAdquisicionDto updateDto, string usuario)
    {
        var adquisicion = await _adquisicionRepository.GetQueryable()
            .Include(a => a.UnidadAdministrativa)
            .Include(a => a.TipoBienServicio)
            .Include(a => a.Proveedor)
            .FirstOrDefaultAsync(a => a.Id == id);
        
        // Guardar valores anteriores para el historial
        var valorAnterior = System.Text.Json.JsonSerializer.Serialize(adquisicion);
        
        _mapper.Map(updateDto, adquisicion);
        adquisicion.ValorTotal = updateDto.Cantidad * updateDto.ValorUnitario;
        
        await _adquisicionRepository.UpdateAsync(adquisicion);
        
        await RegistrarHistorial(id, "Actualización", 
            valorAnterior, 
            System.Text.Json.JsonSerializer.Serialize(adquisicion), 
            usuario);
    }

    public async Task DesactivarAsync(int id, string usuario)
    {
        var adquisicion = await _adquisicionRepository.GetByIdAsync(id);
        var estadoAnterior = adquisicion.Estado;
        
        adquisicion.Estado = "inactivo";
        await _adquisicionRepository.UpdateAsync(adquisicion);
        
        await RegistrarHistorial(id, "Estado", 
            estadoAnterior, 
            "inactivo", 
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