using AddToCartAPI.Model.Models;
using Microsoft.EntityFrameworkCore;

namespace AddToCartAPI.Model.DataContext
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<CartProduct> CartProducts { get; set; }
        public virtual DbSet<Cart> Carts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity(typeof(Product), b =>
            {
                b.Property<long>("ProductId")
                    .ValueGeneratedOnAdd();

                b.HasKey("ProductId");

                b.HasIndex("ProductId");

                b.HasData(
                    new Product { ProductId = 1, ProductName = "test_product1", Price = 150, Stock = 1000, VariantId = 1, CategoryId = 1 },
                    new Product { ProductId = 2, ProductName = "test_product2", Price = 150, Stock = 1000, VariantId = 1, CategoryId = 1 },
                    new Product { ProductId = 3, ProductName = "test_product3", Price = 150, Stock = 1000, VariantId = 1, CategoryId = 1 },
                    new Product { ProductId = 4, ProductName = "test_product4", Price = 350, Stock = 500, VariantId = 3, CategoryId = 2 },
                    new Product { ProductId = 5, ProductName = "test_product5", Price = 50, Stock = 750, VariantId = 4, CategoryId = 3 },
                    new Product { ProductId = 6, ProductName = "test_product6", Price = 120, Stock = 250, VariantId = 1, CategoryId = 2 },
                    new Product { ProductId = 7, ProductName = "test_product7", Price = 290, Stock = 250, VariantId = 1, CategoryId = 3 },
                    new Product { ProductId = 8, ProductName = "test_product8", Price = 40, Stock = 250, VariantId = 1, CategoryId = 4 },
                    new Product { ProductId = 9, ProductName = "test_product9", Price = 10, Stock = 250, VariantId = 1, CategoryId = 1 },
                    new Product { ProductId = 10, ProductName = "test_product10", Price = 30, Stock = 250, VariantId = 1, CategoryId = 1 }
                    );

            });
            modelBuilder.Entity<Cart>(b =>
            {
                b.Property<long>("Id")
                    .ValueGeneratedOnAdd();

                b.HasKey("Id");

                b.HasMany(x => x.Items);
            });

            modelBuilder.Entity<CartProduct>(b =>
            {
                b.HasKey("Id");

                b.Property<long>("Id")
                    .ValueGeneratedOnAdd();

                b.Property<long>("CartId");

                b.Property<long>("ProductId");

                b.Property<int>("Quantity");

                b.HasIndex("CartId");

                b.HasOne(x => x.Cart)
                    .WithMany("Items")
                    .HasForeignKey("CartId")
                    .OnDelete(DeleteBehavior.Restrict);

                b.HasOne(x => x.Product)
                    .WithMany()
                    .HasForeignKey("ProductId")
                    .OnDelete(DeleteBehavior.Restrict);

            });

        }
    }
}
