using ECommerce.DAL.Contractors;
using ECommerce.DAL.DataAccess;
using ECommerce.DAL.DataAccess.Entities;

namespace ECommerce.DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ECommerceDbContext _db;
        private IBaseRepository<User>? userRepo;
        public UnitOfWork(ECommerceDbContext db) => _db = db;

        public IBaseRepository<User> UserRepository
        {
            get
            {
                if (userRepo == null)
                    userRepo = new BaseRepository<User>(_db);

                return userRepo;
            }
        }

        public void Dispose()
        {
            if(_db != null)
                _db.Dispose();

            userRepo = null;
        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}
