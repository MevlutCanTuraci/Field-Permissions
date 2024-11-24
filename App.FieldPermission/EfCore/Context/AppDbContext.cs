using EfCore.Entities;
using Microsoft.EntityFrameworkCore;
namespace EfCore.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        modelBuilder.AddHasProducts();
        
        base.OnModelCreating(modelBuilder);
    }

    public DbSet<EfFieldPermissions> FieldPermissions { get; set; }
    public DbSet<EfProducts> Products { get; set; }
}

public static class DbContextExtensions
{
    public static void AddHasProducts(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EfProducts>().HasData(new List<EfProducts>
        {
            new EfProducts
            {
                Id = 1,
                BasicUnit = 5,
                CreatedDate = new DateTime(2024, 8, 26, 13, 0, 15),
                Name = "Product 1",
                Price = 29.09m,
                Sku = "Sku_randomvalue1",
                UpdatedDate = new DateTime(2024, 11, 4, 13, 0, 15)
            },
            new EfProducts
            {
                Id = 2,
                BasicUnit = 9,
                CreatedDate = new DateTime(2024, 10, 12, 13, 0, 15),
                Name = "Product 2",
                Price = 83.70m,
                Sku = "Sku_randomvalue2",
                UpdatedDate = new DateTime(2024, 11, 16, 13, 0, 15)
            },
            new EfProducts
            {
                Id = 3,
                BasicUnit = 1,
                CreatedDate = new DateTime(2024, 11, 6, 13, 0, 15),
                Name = "Product 3",
                Price = 53.17m,
                Sku = "Sku_randomvalue3",
                UpdatedDate = new DateTime(2024, 10, 20, 13, 0, 15)
            },
            new EfProducts
            {
                Id = 4,
                BasicUnit = 3,
                CreatedDate = new DateTime(2024, 9, 27, 13, 0, 15),
                Name = "Product 4",
                Price = 19.42m,
                Sku = "Sku_randomvalue4",
                UpdatedDate = new DateTime(2024, 11, 4, 13, 0, 15)
            },
            new EfProducts
            {
                Id = 5,
                BasicUnit = 3,
                CreatedDate = new DateTime(2024, 8, 27, 13, 0, 15),
                Name = "Product 5",
                Price = 44.10m,
                Sku = "Sku_randomvalue5",
                UpdatedDate = new DateTime(2024, 11, 4, 13, 0, 15)
            }
        });
    }
}