namespace ECommerce.DAL.Contractors
{
    public interface IBaseRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        T? GetFirstByCondition(Func<T, bool> condition);
        Task<T> GetByIDAsync(object id);
        Task InsertAsync(T entity);
        Task UpdateAsync(object id, object model);
        Task DeleteAsync(object id);
    }
}