﻿// <auto-generated />
using ImageGalley.Data.AppDbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace ImageGalley.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ImageGallery.Core.Model.ImageModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FileName");

                    b.HasKey("Id");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("ImageGallery.Core.Model.ProductImageMappingModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("ImageId");

                    b.Property<int>("Position");

                    b.Property<Guid>("ProductId");

                    b.Property<int>("SortOrder");

                    b.HasKey("Id");

                    b.HasIndex("ImageId");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductImageMapping");
                });

            modelBuilder.Entity("ImageGallery.Core.Model.ProductModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateAdded");

                    b.Property<DateTime>("DateModified");

                    b.Property<string>("Description");

                    b.Property<bool>("DisplayAvailability");

                    b.Property<int>("MaximumCartQuantity");

                    b.Property<string>("MetaDescription");

                    b.Property<string>("MetaKeywords");

                    b.Property<string>("MetaTitle");

                    b.Property<int>("MinimumCartQuantity");

                    b.Property<int>("MinimumStockQuantity");

                    b.Property<string>("Name");

                    b.Property<int>("NotifyForQuantityBelow");

                    b.Property<decimal>("Price");

                    b.Property<bool>("Published");

                    b.Property<string>("SeoUrl");

                    b.Property<decimal?>("SpecialPrice");

                    b.Property<DateTime?>("SpecialPriceEndDate");

                    b.Property<DateTime?>("SpecialPriceStartDate");

                    b.Property<int>("StockQuantity");

                    b.HasKey("Id");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("ImageGallery.Core.Model.ProductImageMappingModel", b =>
                {
                    b.HasOne("ImageGallery.Core.Model.ImageModel", "Image")
                        .WithMany()
                        .HasForeignKey("ImageId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ImageGallery.Core.Model.ProductModel", "Product")
                        .WithMany("Images")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}