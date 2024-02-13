using Infrastructure.Database;
using Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly AppDbContext _context;
    private readonly DbSet<T> _table;

    public GenericRepository(AppDbContext context)
    {
        _context = context;
        _table = _context.Set<T>();
    }

    public async Task<T> Add(T entity)
    {
        await _table.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<T?> Delete(int id)
    {
        var entity = await _table.FindAsync(id);
        if (entity is null)
        {
            return null;
        }

        _table.Remove(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<IEnumerable<T>?> GetAllPaged(int offset = 0, int takeCount = 100)
    {
        return await _table.Skip(offset).Take(takeCount).ToListAsync();
    }

    public async Task<T?> GetById(int id)
    {
        return await _table.FindAsync(id);
    }

    public async Task<T?> Update(int id, T entity)
    {
        var entityFromDb = await _table.FindAsync(id);
        if (entityFromDb is null)
        {
            return null;
        }

        _table.Update(entity);
        await _context.SaveChangesAsync();
        return entity;
    }
}
