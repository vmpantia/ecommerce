namespace ECommerce.DAL.Contractors
{
    public interface IBaseRepository<T> where T : class
    {
        Task DeleteAsync(object id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIDAsync(object id);
        Task InsertAsync(T entity);
        Task UpdateAsync(object id, object model);
    }
}