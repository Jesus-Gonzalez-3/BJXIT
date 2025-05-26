using InventariosModel.Seeds;
using Microsoft.EntityFrameworkCore;

namespace InventariosModel.Models
{
    public class InventariosContext : DbContext
    {
        public InventariosContext(DbContextOptions options) : base(options) { }

        public DbSet<UsersModel> UserModel { get; set; }
        public DbSet<RolesModel> RoleModel { get; set; }
        public DbSet<ProductModel> ProductModel { get; set; }
        public DbSet<OrdersModel> OrdersModel { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UsersModel>().Property(x=> x.UserStatus).HasDefaultValueSql("1");
            modelBuilder.Entity<OrdersModel>().Property(x=> x.OrderStatus).HasDefaultValueSql("1");
            modelBuilder.Entity<RolesModel>().Property(x=> x.RoleStatus).HasDefaultValueSql("1");
            modelBuilder.Entity<ProductModel>().Property(x=> x.ProductStatus).HasDefaultValueSql("1");
            modelBuilder.ApplyConfiguration(new RoleSeed());

            base.OnModelCreating(modelBuilder);
        }

    }
}
