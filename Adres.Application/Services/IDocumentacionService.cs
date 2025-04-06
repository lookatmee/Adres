using Adres.Application.DTOs;

namespace Adres.Application.Services;

public interface IDocumentacionService
{
    Task<DocumentacionDto> CreateAsync(CreateDocumentacionDto createDto);
    Task<DocumentacionDto?> GetByIdAsync(int id);
    Task<IEnumerable<DocumentacionDto>> GetByAdquisicionIdAsync(int adquisicionId);
    Task<IEnumerable<DocumentacionDto>> GetAllAsync();
    Task<bool> DeleteAsync(int id);
} 