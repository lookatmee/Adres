using AutoMapper;
using Adres.Domain.Common;
using Adres.Domain.Entities;
using Adres.Application.DTOs;

namespace Adres.Application.Services;

public interface ITipoBienServicioService
{
    Task<TipoBienServicioDto> CreateAsync(CreateTipoBienServicioDto createDto);
    Task<TipoBienServicioDto> GetByIdAsync(int id);
    Task<IEnumerable<TipoBienServicioDto>> GetAllAsync();
    Task UpdateAsync(int id, UpdateTipoBienServicioDto updateDto);
}

public class TipoBienServicioService : ITipoBienServicioService
{
    private readonly IRepository<TipoBienServicio> _repository;
    private readonly IMapper _mapper;

    public TipoBienServicioService(IRepository<TipoBienServicio> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<TipoBienServicioDto> CreateAsync(CreateTipoBienServicioDto createDto)
    {
        var entity = _mapper.Map<TipoBienServicio>(createDto);
        var result = await _repository.AddAsync(entity);
        return _mapper.Map<TipoBienServicioDto>(result);
    }

    public async Task<TipoBienServicioDto> GetByIdAsync(int id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return _mapper.Map<TipoBienServicioDto>(entity);
    }

    public async Task<IEnumerable<TipoBienServicioDto>> GetAllAsync()
    {
        var entities = await _repository.GetAllAsync();
        return _mapper.Map<IEnumerable<TipoBienServicioDto>>(entities);
    }

    public async Task UpdateAsync(int id, UpdateTipoBienServicioDto updateDto)
    {
        var entity = await _repository.GetByIdAsync(id);
        _mapper.Map(updateDto, entity);
        await _repository.UpdateAsync(entity);
    }
} 