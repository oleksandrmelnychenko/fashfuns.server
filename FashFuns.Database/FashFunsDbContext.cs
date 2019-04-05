using FashFuns.Common;
using FashFuns.Database.TableMaps;
using FashFuns.Database.TableMaps.Identity;
using FashFuns.Database.TableMaps.Product;
using FashFuns.Domain.Entities.Identity;
using FashFuns.Domain.Entities.Products;
using Microsoft.EntityFrameworkCore;

namespace FashFuns.Database
{
    public class FashFunsDbContext : DbContext
    {
        public FashFunsDbContext() { }

        public FashFunsDbContext(
            DbContextOptions<FashFunsDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConfigurationManager.LocalDatabaseConnectionString);
        }

        public DbSet<ProductCategory> ProductCategories { get; set; }

        public DbSet<ShoppingCart> ShoppingCarts { get; set; }

        public DbSet<OrderItem> OrderItems { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<UserIdentity> UserIdentities { get; set; }

        public DbSet<UserRole> UserRoles { get; set; }

        public DbSet<UserIdentityRoleType> UserIdentityRoleTypes { get; set; }

        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.AddConfiguration(new UserIdentityMap());
            modelBuilder.AddConfiguration(new UserIdentityRoleTypeMap());
            modelBuilder.AddConfiguration(new UserRoleMap());
            modelBuilder.AddConfiguration(new ProductMap());
            modelBuilder.AddConfiguration(new OrderItemMap());
            modelBuilder.AddConfiguration(new ShoppingCartMap());
            modelBuilder.AddConfiguration(new UserProductMapMap());
            modelBuilder.AddConfiguration(new ProductCategoryMap());
            modelBuilder.AddConfiguration(new OrderMap());
        }
    }
}
