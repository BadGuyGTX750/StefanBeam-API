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

        public DbSet<TopCategoryModel> topCategories { get; set; }

        public DbSet<MiddleCategoryModel> middleCategories { get; set; }

        public DbSet<SubCategoryModel> subCategories { get; set; }

        public DbSet<ProductModel> products { get; set; }

        public DbSet<WeightPriceModel> weightPrices { get; set; }

        public DbSet<FlavorQuantityModel> flavorQuantities { get; set; }

        public DbSet<PhotoAttachmentModel> photoAttachments { get; set; }

        public DbSet<AccesToken> accesTokens { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TopCategoryModel>()
                .HasMany(u => u.middleCategories)
                .WithOne(u => u.topCategory)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MiddleCategoryModel>()
                .HasMany(u => u.subCategories)
                .WithOne(u => u.middleCategory)
                .OnDelete(DeleteBehavior.Restrict);

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
                .HasMany(u => u.photoAttachments)
                .WithOne(u => u.product)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
