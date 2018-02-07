using System;
using System.Collections.Generic;
using System.Text;
using ImageGallery.Core.Model;
using Microsoft.EntityFrameworkCore;

namespace ImageGalley.Data.AppDbContext
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<ProductModel> ProductModels { get; set; }
        public DbSet<ImageModel> ImageModels { get; set; }
        public DbSet<ProductImageMappingModel> ProductImageMappingModels { get; set; }


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
          
            base.OnModelCreating(modelBuilder);
        }
    }
}
