using ECommerce.DAL.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.DAL.DataAccess
{
    public class ECommerceDbContext : DbContext
    {
        public ECommerceDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
