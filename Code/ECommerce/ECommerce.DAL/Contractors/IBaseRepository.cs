namespace ECommerce.DAL.Contractors
{
    public interface IBaseRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        IEnumerable<T> GetByCondition(Func<T, bool> condition);
        Task<T> GetByIDAsync(object id);
        Task InsertAsync(T entity);
        Task UpdateAsync(object id, object model);
        Task DeleteAsync(object id);
        bool IsExist(Func<T, bool> condition);
    }
}