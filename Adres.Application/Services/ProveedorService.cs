using AutoMapper;
using Adres.Domain.Common;
using Adres.Domain.Entities;
using Adres.Application.DTOs;

namespace Adres.Application.Services;

public interface IProveedorService
{
    Task<ProveedorDto> CreateAsync(CreateProveedorDto createDto);
    Task<ProveedorDto> GetByIdAsync(int id);
    Task<IEnumerable<ProveedorDto>> GetAllAsync();
    Task UpdateAsync(int id, UpdateProveedorDto updateDto);
}

public class ProveedorService : IProveedorService
{
    private readonly IRepository<Proveedor> _repository;
    private readonly IMapper _mapper;

    public ProveedorService(IRepository<Proveedor> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ProveedorDto> CreateAsync(CreateProveedorDto createDto)
    {
        var entity = _mapper.Map<Proveedor>(createDto);
        var result = await _repository.AddAsync(entity);
        return _mapper.Map<ProveedorDto>(result);
    }

    public async Task<ProveedorDto> GetByIdAsync(int id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return _mapper.Map<ProveedorDto>(entity);
    }

    public async Task<IEnumerable<ProveedorDto>> GetAllAsync()
    {
        var entities = await _repository.GetAllAsync();
        return _mapper.Map<IEnumerable<ProveedorDto>>(entities);
    }

    public async Task UpdateAsync(int id, UpdateProveedorDto updateDto)
    {
        var entity = await _repository.GetByIdAsync(id);
        _mapper.Map(updateDto, entity);
        await _repository.UpdateAsync(entity);
    }
} 