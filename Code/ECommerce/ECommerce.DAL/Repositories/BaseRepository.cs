using ECommerce.Common.Constants.Messages;
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

        public IEnumerable<T> GetByCondition(Func<T, bool> condition)
        {
            return _table.Where(condition).ToList();
        }

        public async Task<T> GetByIDAsync(object id)
        {
            var result = await _table.FindAsync(id);
            if (result == null)
                throw new Exception(ErrorMessage.NO_DATA_FOUND);

            return result;
        }

        public async Task InsertAsync(T entity)
        {
            await _table.AddAsync(entity);
        }

        public async Task UpdateAsync(object id, object model)
        {
            var result = await GetByIDAsync(id);
            _db.Entry(result).CurrentValues.SetValues(model);
        }

        public async Task DeleteAsync(object id)
        {
            var result = await GetByIDAsync(id);
            _table.Remove(result);
        }
    }
}
