using ECommerce.DAL.DataAccess.Entities;

namespace ECommerce.DAL.Contractors
{
    public interface IUnitOfWork
    {
        IBaseRepository<User> UserRepository { get; }
        IBaseRepository<Product> ProductRepository { get; }
        Task SaveAsync();
    }
}