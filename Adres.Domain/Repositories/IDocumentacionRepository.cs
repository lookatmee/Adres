using Adres.Domain.Entities;

namespace Adres.Domain.Repositories;

public interface IDocumentacionRepository
{
    Task<IEnumerable<Documentacion>> GetAllAsync();
    Task<Documentacion?> GetByIdAsync(int id);
    Task<IEnumerable<Documentacion>> GetByAdquisicionIdAsync(int adquisicionId);
    Task<Documentacion> AddAsync(Documentacion entity);
    Task UpdateAsync(Documentacion entity);
    Task DeleteAsync(Documentacion entity);
} 