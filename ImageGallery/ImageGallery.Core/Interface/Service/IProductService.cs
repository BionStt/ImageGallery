using System;
using System.Collections.Generic;
using System.Linq;
using ImageGallery.Core.Model;

namespace ImageGallery.Core.Interface.Service
{
    public interface IProductService
    {
       
        IList<ProductModel> GetAllProducts();
        ProductModel GetProductById(Guid id);
        ProductModel GetProductBySeo(string seo);
        void InsertProduct(ProductModel product);
        void UpdateProduct(ProductModel product);
        void DeleteProducts(IList<Guid> ids);
        IList<ProductModel> SearchProduct(
            string nameFilter = null,
            string seoFilter = null,
            string[] categoryFilter = null,
            string[] manufacturerFilter = null,
            string[] priceFilter = null,
            bool isPublished = true);

        IQueryable<ProductModel> Table();
    }
}
