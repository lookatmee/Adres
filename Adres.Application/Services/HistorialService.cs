using AutoMapper;
using Adres.Domain.Entities;
using Adres.Application.DTOs;
using Adres.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace Adres.Application.Services;

public interface IHistorialService
{
    Task<IEnumerable<HistorialCambioDto>> GetAllAsync();
    Task<IEnumerable<HistorialCambioDto>> GetByAdquisicionIdAsync(int adquisicionId);
}

public class HistorialService : IHistorialService
{
    private readonly IRepository<HistorialCambios> _historialRepository;
    private readonly IMapper _mapper;

    public HistorialService(IRepository<HistorialCambios> historialRepository, IMapper mapper)
    {
        _historialRepository = historialRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<HistorialCambioDto>> GetAllAsync()
    {
        var historial = await _historialRepository.GetQueryable()
            .OrderByDescending(h => h.FechaCambio)
            .ToListAsync();

        return _mapper.Map<IEnumerable<HistorialCambioDto>>(historial);
    }

    public async Task<IEnumerable<HistorialCambioDto>> GetByAdquisicionIdAsync(int adquisicionId)
    {
        var historial = await _historialRepository.GetQueryable()
            .Where(h => h.AdquisicionId == adquisicionId)
            .OrderByDescending(h => h.FechaCambio)
            .ToListAsync();

        return _mapper.Map<IEnumerable<HistorialCambioDto>>(historial);
    }
} 