using Microsoft.EntityFrameworkCore;
using ShopApi.Configuration;
using ShopApi.Entites;

namespace ShopApi.Context;

public class ProductDbContext : DbContext
{
    public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options)
    {
        Database.EnsureCreated();   // Create database if doesn't exist
    }

    public DbSet<User> Users { get; set; }
    public DbSet<UserProfile> UserProfiles { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductFile> ProductFiles { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Review> Reviews { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new OrderConfiguration());
        modelBuilder.ApplyConfiguration(new OrderItemConfiguration());
        modelBuilder.ApplyConfiguration(new ProductConfiguration());
        modelBuilder.ApplyConfiguration(new CategoryConfiguration());
        modelBuilder.ApplyConfiguration(new ReviewConfiguration());
    }
}
