using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MuiltyShop.Models.Checkout;
using MuiltyShop.Models.Contact;
using MuiltyShop.Models.Photo;
using MuiltyShop.Models.Product;
using MuiltyShop.Models.Product.Category;
using MuiltyShop.Models.Product.Color;
using MuiltyShop.Models.Product.Size;
using System.Reflection.Emit;

namespace MuiltyShop.Models
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
           
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            base.OnConfiguring(builder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var tableName = entityType.GetTableName();
                if (tableName.StartsWith("AspNet"))
                {
                    entityType.SetTableName(tableName.Substring(6));
                }
            }

            // Fluent APT

            // Product
            modelBuilder.Entity<CategoryProductModel>(entity =>
            {
                entity.HasIndex(c => c.Slug)
                      .IsUnique();
            });

            modelBuilder.Entity<ProductCategoryProductModel>(entity =>
            {
                entity.HasKey(c => new { c.ProductID, c.CategoryID });
            });

            modelBuilder.Entity<ProductModel>(entity =>
            {
                entity.HasIndex(p => p.Slug)
                      .IsUnique();
            });

            // Color
            /* Creating a composite key for the ProductColorModel. */
            modelBuilder.Entity<ProductColorModel>(entity =>
            {
                entity.HasKey(c => new { c.ProductID, c.ColorID });
            });
            //SIze
            modelBuilder.Entity<ProductSizeModel>(entity =>
            {
                entity.HasKey(c => new { c.ProductID, c.SizeID });
            });
        }
        // Contact
        public DbSet<ContactModel> Contacts { get; set; }

        // Product
        public DbSet<CategoryProductModel> CategoryProducts { get; set; }

        public DbSet<ProductModel> Products { get; set; }

        public DbSet<ProductCategoryProductModel> ProductCategoryProducts { get; set; }
        // Product Photo
        public DbSet<PhotoModel> Photos { get; set; }

        // Color
        public DbSet<ColorModel> Colors { get; set; }

        public DbSet<ProductColorModel> ProductColors { get; set; }
        // Size
        public DbSet<SizeModel> Sizes { get; set; }

        public DbSet<ProductSizeModel> ProductSizes { get; set; }

        // Payment
        public DbSet<PaymentModel> Payments { get; set; }

        // Checkout
        public DbSet<CheckoutModel> Checkouts { get; set; }
    }
}
