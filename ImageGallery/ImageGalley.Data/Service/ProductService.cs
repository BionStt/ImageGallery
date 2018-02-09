using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ImageGallery.Core.Interface.Base;
using ImageGallery.Core.Interface.Service;
using ImageGallery.Core.Model;
using ImageGalley.Data.AppDbContext;
using Microsoft.EntityFrameworkCore;

namespace ImageGalley.Data.Service
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IRepository<ProductModel> _productRepository;

        public ProductService(ApplicationDbContext dbContext, IRepository<ProductModel> productRepository)
        {
            _dbContext = dbContext;
            _productRepository = productRepository;
        }
        public IList<ProductModel> GetAllProducts()
        {
            return _dbContext.ProductModels
                .Include(x => x.Images).ThenInclude(x => x.Image)
                .AsNoTracking()
                .ToList();
        }

        public ProductModel GetProductById(Guid id)
        {
            return _dbContext.ProductModels
                .Include(x => x.Images).ThenInclude(x => x.Image)
                .AsNoTracking()
                .SingleOrDefault(x => x.Id == id);
        }

        public ProductModel GetProductBySeo(string seo)
        {
            return _dbContext.ProductModels
                .Include(x => x.Images).ThenInclude(x => x.Image)
                .AsNoTracking()
                .SingleOrDefault(x => x.SeoUrl == seo);
        }

        public void InsertProduct(ProductModel product)
        {
            if (product == null)
                throw new ArgumentNullException("product");

            _productRepository.Insert(product);
            _productRepository.SaveChanges();
        }

        public void UpdateProduct(ProductModel product)
        {
            if (product == null)
                throw new ArgumentNullException("product");

            _productRepository.Update(product);
            _productRepository.SaveChanges();
        }

        public void DeleteProducts(IList<Guid> ids)
        {
            if (ids == null)
                throw new ArgumentNullException("ids");

            foreach (var id in ids)
                _productRepository.Delete(GetProductById(id));

            _productRepository.SaveChanges();
        }

        public IList<ProductModel> SearchProduct(string nameFilter = null, string seoFilter = null, string[] categoryFilter = null,
            string[] manufacturerFilter = null, string[] priceFilter = null, bool isPublished = true)
        {
            throw new NotImplementedException();
        }

        public IQueryable<ProductModel> Table()
        {
            return _dbContext.ProductModels;
        }
    }
}
