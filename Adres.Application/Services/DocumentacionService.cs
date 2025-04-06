using AutoMapper;
using Adres.Domain.Common;
using Adres.Domain.Entities;
using Adres.Application.DTOs;
using Adres.Domain.Repositories;

namespace Adres.Application.Services;

public class DocumentacionService : IDocumentacionService
{
    private readonly IDocumentacionRepository _repository;
    private readonly IMapper _mapper;

    public DocumentacionService(IDocumentacionRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<DocumentacionDto> CreateAsync(CreateDocumentacionDto createDto)
    {
        var entity = _mapper.Map<Documentacion>(createDto);
        var result = await _repository.AddAsync(entity);
        return _mapper.Map<DocumentacionDto>(result);
    }

    public async Task<DocumentacionDto?> GetByIdAsync(int id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return entity != null ? _mapper.Map<DocumentacionDto>(entity) : null;
    }

    public async Task<IEnumerable<DocumentacionDto>> GetByAdquisicionIdAsync(int adquisicionId)
    {
        var entities = await _repository.GetAllAsync();
        var filtered = entities.Where(d => d.AdquisicionId == adquisicionId);
        return _mapper.Map<IEnumerable<DocumentacionDto>>(filtered);
    }

    public async Task<IEnumerable<DocumentacionDto>> GetAllAsync()
    {
        var entities = await _repository.GetAllAsync();
        return _mapper.Map<IEnumerable<DocumentacionDto>>(entities);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var documento = await _repository.GetByIdAsync(id);
        if (documento == null)
        {
            return false;
        }

        await _repository.DeleteAsync(documento);
        return true;
    }
} 