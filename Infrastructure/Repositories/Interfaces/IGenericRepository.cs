namespace Infrastructure.Repositories.Interfaces;

public interface IGenericRepository<T> where T : class
{
    public Task<IEnumerable<T>?> GetAllPaged(int offset = 0, int takeCount = 100);

    public Task<T?> GetById(int id);

    public Task<T> Add(T entity);

    public Task<T?> Update(int id, T entity);

    public Task<T?> Delete(int id);
}
