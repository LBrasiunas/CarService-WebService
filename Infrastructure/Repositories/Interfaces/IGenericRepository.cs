namespace Infrastructure.Repositories.Interfaces;

public interface IGenericRepository<T> where T : class
{
    public Task<IEnumerable<T>?> GetAllPaged(int offset, int takeCount);

    public Task<T?> GetById(int id);

    public Task<T?> GetByCombinedId(int id1, int id2);

    public Task<T> Add(T entity);

    public Task<T?> Update(T entity);

    public Task<T?> Delete(int id);

    public Task<T?> DeleteByCombinedId(int id1, int id2);

    public Task Detach(T entity);
}
