﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RestApplication.Data;

#nullable disable

namespace RestApplication.Data.Migrations
{
    [DbContext(typeof(Entities))]
    [Migration("20230322111134_initial13")]
    partial class initial13
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("RestApplication.Models.AppUser.AccesToken", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("accesToken")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("expirationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("refreshToken")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("accesTokens");
                });

            modelBuilder.Entity("RestApplication.Models.AppUser.AppUserModel", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("firstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("isVerified")
                        .HasColumnType("bit");

                    b.Property<string>("lastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("tokenVerifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("verificationToken")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("verificationTokenCreationDate")
                        .HasColumnType("datetime2");

                    b.HasKey("id");

                    b.ToTable("appUsers");
                });

            modelBuilder.Entity("RestApplication.Models.Attachment.PhotoAttachmentModel", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ext")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("filePath")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("parentProductId")
                        .IsRequired()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("productName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.HasIndex("parentProductId");

                    b.ToTable("photoAttachments");
                });

            modelBuilder.Entity("RestApplication.Models.Category.MiddleCategoryModel", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("parentCategoryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("parentCategoryName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.HasIndex("parentCategoryId");

                    b.ToTable("middleCategories");
                });

            modelBuilder.Entity("RestApplication.Models.Category.SubCategoryModel", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("parentCategoryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("parentCategoryName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.HasIndex("parentCategoryId");

                    b.ToTable("subCategories");
                });

            modelBuilder.Entity("RestApplication.Models.Category.TopCategoryModel", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("parentCategoryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("parentCategoryName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("topCategories");
                });

            modelBuilder.Entity("RestApplication.Models.Product.FlavorQuantityModel", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("flavor")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("productId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<long>("quantity")
                        .HasColumnType("bigint");

                    b.HasKey("id");

                    b.HasIndex("productId");

                    b.ToTable("flavorQuantities");
                });

            modelBuilder.Entity("RestApplication.Models.Product.ProductModel", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("categoryName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("isInStock")
                        .HasColumnType("bit");

                    b.Property<string>("longDescr")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("parentCategoryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("shortDescr")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.HasIndex("parentCategoryId");

                    b.ToTable("products");
                });

            modelBuilder.Entity("RestApplication.Models.Product.WeightPriceModel", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("currency")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("price")
                        .HasColumnType("int");

                    b.Property<Guid>("productId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("weight")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.HasIndex("productId");

                    b.ToTable("weightPrices");
                });

            modelBuilder.Entity("RestApplication.Models.Attachment.PhotoAttachmentModel", b =>
                {
                    b.HasOne("RestApplication.Models.Product.ProductModel", "product")
                        .WithMany("photoAttachments")
                        .HasForeignKey("parentProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("product");
                });

            modelBuilder.Entity("RestApplication.Models.Category.MiddleCategoryModel", b =>
                {
                    b.HasOne("RestApplication.Models.Category.TopCategoryModel", "topCategory")
                        .WithMany("middleCategories")
                        .HasForeignKey("parentCategoryId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("topCategory");
                });

            modelBuilder.Entity("RestApplication.Models.Category.SubCategoryModel", b =>
                {
                    b.HasOne("RestApplication.Models.Category.MiddleCategoryModel", "middleCategory")
                        .WithMany("subCategories")
                        .HasForeignKey("parentCategoryId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("middleCategory");
                });

            modelBuilder.Entity("RestApplication.Models.Product.FlavorQuantityModel", b =>
                {
                    b.HasOne("RestApplication.Models.Product.ProductModel", "product")
                        .WithMany("flavor_quantity")
                        .HasForeignKey("productId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("product");
                });

            modelBuilder.Entity("RestApplication.Models.Product.ProductModel", b =>
                {
                    b.HasOne("RestApplication.Models.Category.SubCategoryModel", "subCategory")
                        .WithMany("products")
                        .HasForeignKey("parentCategoryId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("subCategory");
                });

            modelBuilder.Entity("RestApplication.Models.Product.WeightPriceModel", b =>
                {
                    b.HasOne("RestApplication.Models.Product.ProductModel", "product")
                        .WithMany("weight_price")
                        .HasForeignKey("productId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("product");
                });

            modelBuilder.Entity("RestApplication.Models.Category.MiddleCategoryModel", b =>
                {
                    b.Navigation("subCategories");
                });

            modelBuilder.Entity("RestApplication.Models.Category.SubCategoryModel", b =>
                {
                    b.Navigation("products");
                });

            modelBuilder.Entity("RestApplication.Models.Category.TopCategoryModel", b =>
                {
                    b.Navigation("middleCategories");
                });

            modelBuilder.Entity("RestApplication.Models.Product.ProductModel", b =>
                {
                    b.Navigation("flavor_quantity");

                    b.Navigation("photoAttachments");

                    b.Navigation("weight_price");
                });
#pragma warning restore 612, 618
        }
    }
}
