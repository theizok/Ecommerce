using Ecommerce.Shared.Entities;
using Microsoft.EntityFrameworkCore;
namespace Ecommerce.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) :base(options) {
        }
        
        public DbSet<Country> Countries { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Category> Categories { get; set;}
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductsImages { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Country>().HasIndex(x =>x.Name).IsUnique();
            modelBuilder.Entity<State>().HasIndex("Name","CountryId").IsUnique();
            modelBuilder.Entity<City>().HasIndex("Name","StateId").IsUnique();
            modelBuilder.Entity<ProductCategory>().HasIndex(x => x.Name).IsUnique();
            modelBuilder.Entity<Category>().HasIndex("Name","ProductCategoryId").IsUnique();
            modelBuilder.Entity<Product>().HasIndex("Name","ProductCategoryId").IsUnique();

        }

    }
}
