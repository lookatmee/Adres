using Adres.Application.DTOs;
using Adres.Domain.Common;
using Adres.Domain.Entities;
using AutoMapper;

namespace Adres.Application.Services;

public interface IUnidadAdministrativaService
{
    Task<UnidadAdministrativaDto> CreateAsync(CreateUnidadAdministrativaDto createDto);
    Task<UnidadAdministrativaDto> GetByIdAsync(int id);
    Task<IEnumerable<UnidadAdministrativaDto>> GetAllAsync();
    Task UpdateAsync(int id, UpdateUnidadAdministrativaDto updateDto);
}

public class UnidadAdministrativaService : IUnidadAdministrativaService
{
    private readonly IRepository<UnidadAdministrativa> _repository;
    private readonly IMapper _mapper;

    public UnidadAdministrativaService(IRepository<UnidadAdministrativa> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<UnidadAdministrativaDto> CreateAsync(CreateUnidadAdministrativaDto createDto)
    {
        var entity = _mapper.Map<UnidadAdministrativa>(createDto);
        var result = await _repository.AddAsync(entity);
        return _mapper.Map<UnidadAdministrativaDto>(result);
    }

    public async Task<UnidadAdministrativaDto> GetByIdAsync(int id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return _mapper.Map<UnidadAdministrativaDto>(entity);
    }

    public async Task<IEnumerable<UnidadAdministrativaDto>> GetAllAsync()
    {
        var entities = await _repository.GetAllAsync();
        return _mapper.Map<IEnumerable<UnidadAdministrativaDto>>(entities);
    }

    public async Task UpdateAsync(int id, UpdateUnidadAdministrativaDto updateDto)
    {
        var entity = await _repository.GetByIdAsync(id);
        _mapper.Map(updateDto, entity);
        await _repository.UpdateAsync(entity);
    }
} 