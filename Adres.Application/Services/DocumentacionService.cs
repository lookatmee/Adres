using AutoMapper;
using Adres.Domain.Common;
using Adres.Domain.Entities;
using Adres.Application.DTOs;

namespace Adres.Application.Services;

public interface IDocumentacionService
{
    Task<DocumentacionDto> CreateAsync(CreateDocumentacionDto createDto);
    Task<IEnumerable<DocumentacionDto>> GetByAdquisicionIdAsync(int adquisicionId);
}

public class DocumentacionService : IDocumentacionService
{
    private readonly IRepository<Documentacion> _repository;
    private readonly IMapper _mapper;

    public DocumentacionService(IRepository<Documentacion> repository, IMapper mapper)
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

    public async Task<IEnumerable<DocumentacionDto>> GetByAdquisicionIdAsync(int adquisicionId)
    {
        var entities = await _repository.GetAllAsync();
        var filtered = entities.Where(d => d.AdquisicionId == adquisicionId);
        return _mapper.Map<IEnumerable<DocumentacionDto>>(filtered);
    }
} 