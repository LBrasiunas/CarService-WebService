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

    public async Task<T?> DeleteByCombinedId(int id1, int id2)
    {
        var entity = await _table.FindAsync(id1, id2);
        if (entity is null)
        {
            return null;
        }

        _table.Remove(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<IEnumerable<T>?> GetAllPaged(int offset, int takeCount)
    {
        return await _table.Skip(offset).Take(takeCount).ToListAsync();
    }

    public async Task<T?> GetByCombinedId(int id1, int id2)
    {
        return await _table.FindAsync(id1, id2);
    }

    public async Task<T?> GetById(int id)
    {
        return await _table.FindAsync(id);
    }

    public async Task<T?> Update(T entity)
    {
        _context.Entry(entity).State = EntityState.Detached;
        _table.Update(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task Detach(T entity)
    {
        _context.Entry(entity).State = EntityState.Detached;
        await _context.SaveChangesAsync();
    }
}
