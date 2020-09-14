using Microsoft.EntityFrameworkCore;

namespace ComputerWordStore.Models.Products
{
    public class ComputersWorldContext: DbContext
    {
        
        public DbSet<Category> Categories { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<BrandCategory> BrandCategories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImages> ProductImageses { get; set; }

        public ComputersWorldContext(DbContextOptions<ComputersWorldContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasIndex(u => u.Name).IsUnique();
            modelBuilder.Entity<Category>().HasIndex(u => u.Slug).IsUnique();
            modelBuilder.Entity<Brand>().HasIndex(b => b.Name).IsUnique();
            modelBuilder.Entity<Brand>().HasIndex(b => b.Slug).IsUnique();
            modelBuilder.Entity<Product>().HasIndex(p => p.Slug).IsUnique();
        }
    }
}