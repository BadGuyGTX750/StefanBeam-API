using Microsoft.EntityFrameworkCore;
using RestApplication.Controllers;
using RestApplication.Models.AppUser;
using RestApplication.Models.Attachment;
using RestApplication.Models.Category;
using RestApplication.Models.Product;

namespace RestApplication.Data
{
    public class Entities : DbContext
    {
        public Entities(DbContextOptions<Entities> options) : base(options)
        {
        }

        public DbSet<AppUserModel> appUsers { get; set; }

        public DbSet<SubCategoryModel> subCategories { get; set; }

        public DbSet<ProductModel> products { get; set; }

        public DbSet<WeightPriceModel> weightPrices { get; set; }

        public DbSet<FlavorQuantityModel> flavorQuantities { get; set; }

        public DbSet<PhotoAttachmentModel> photoAttachments { get; set; }

        public DbSet<AccesToken> accesTokens { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SubCategoryModel>()
                .HasMany(u => u.products)
                .WithOne(u => u.subCategory)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ProductModel>()
                .HasMany(u => u.weight_price)
                .WithOne(u => u.product)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ProductModel>()
                .HasMany(u => u.flavor_quantity)
                .WithOne(u => u.product)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ProductModel>()
                .HasOne(u => u.photoAttachment)
                .WithOne(u => u.product)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
