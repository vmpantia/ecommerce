using ECommerce.DAL.Contractors;
using ECommerce.DAL.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.DAL.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly ECommerceDbContext _db;
        private DbSet<T> _table;
        public BaseRepository(ECommerceDbContext db)
        {
            _db = db;
            _table = db.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _table.ToListAsync();
        }

        public async Task<T> GetByIDAsync(object id)
        {
            var result = await _table.FindAsync(id);
            if (result == null)
                throw new Exception("No data found in the system.");

            return result;
        }

        public async Task InsertAsync(T entity)
        {
            await _table.AddAsync(entity);
        }

        public async Task DeleteAsync(object id)
        {
            var result = await GetByIDAsync(id);
            _table.Remove(result);
        }

        public async Task UpdateAsync(object id, object model)
        {
            var result = await GetByIDAsync(id);
            _db.Entry(result).CurrentValues.SetValues(model);
        }

    }
}
