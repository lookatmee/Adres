using Adres.Domain.Entities;
using Adres.Domain.Repositories;
using Adres.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Adres.Infrastructure.Repositories;

public class DocumentacionRepository : IDocumentacionRepository
{
    private readonly AdresDbContext _context;

    public DocumentacionRepository(AdresDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Documentacion>> GetAllAsync()
    {
        return await _context.Documentaciones
            .Include(d => d.Adquisicion)
            .ToListAsync();
    }

    public async Task<Documentacion?> GetByIdAsync(int id)
    {
        return await _context.Documentaciones
            .Include(d => d.Adquisicion)
            .FirstOrDefaultAsync(d => d.Id == id);
    }

    public async Task<IEnumerable<Documentacion>> GetByAdquisicionIdAsync(int adquisicionId)
    {
        return await _context.Documentaciones
            .Include(d => d.Adquisicion)
            .Where(d => d.AdquisicionId == adquisicionId)
            .ToListAsync();
    }

    public async Task<Documentacion> AddAsync(Documentacion entity)
    {
        _context.Documentaciones.Add(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task UpdateAsync(Documentacion entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Documentacion entity)
    {
        _context.Documentaciones.Remove(entity);
        await _context.SaveChangesAsync();
    }
} 