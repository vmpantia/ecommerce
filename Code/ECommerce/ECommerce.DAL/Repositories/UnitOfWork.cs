using ECommerce.DAL.Contractors;
using ECommerce.DAL.DataAccess;
using ECommerce.DAL.DataAccess.Entities;

namespace ECommerce.DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ECommerceDbContext _db;
        private IBaseRepository<User>? _userRepo;
        private IBaseRepository<Product>? _productRepo;
        public UnitOfWork(ECommerceDbContext db) => _db = db;

        public IBaseRepository<User> UserRepository
        {
            get
            {
                if (_userRepo == null)
                    _userRepo = new BaseRepository<User>(_db);

                return _userRepo;
            }
        }
        public IBaseRepository<Product> ProductRepository
        {
            get
            {
                if (_productRepo == null)
                    _productRepo = new BaseRepository<Product>(_db);

                return _productRepo;
            }
        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }

        public void Dispose()
        {
            if (_db != null)
                _db.Dispose();

            _userRepo = null;
            _productRepo = null;
        }
    }
}
