using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using ImageGallery.Core.Model;

namespace ImageGallery.Core.Interface
{
    public interface IProductRepository
    {
        /// <summary>
        /// Get all products
        /// </summary>
        /// <returns>List of product entities</returns>
        IList<ProductModel> GetAllProducts();

        /// <summary>
        /// Get product usind id
        /// </summary>
        /// <param name="id">Product id</param>
        /// <returns>Product entity</returns>
        ProductModel GetProductById(Guid id);

        /// <summary>
        /// Get product using seo
        /// </summary>
        /// <param name="seo">Product SEO</param>
        /// <returns>Product entity</returns>
        ProductModel GetProductBySeo(string seo);

        /// <summary>
        /// Insert product
        /// </summary>
        /// <param name="product">Product entity</param>
        void InsertProduct(ProductModel product);

        /// <summary>
        /// Update product
        /// </summary>
        /// <param name="product">Product entity</param>
        void UpdateProduct(ProductModel product);

        /// <summary>
        /// Delete products
        /// </summary>
        /// <param name="ids">Ids of product to delete</param>
        void DeleteProducts(IList<Guid> ids);

        /// <summary>
        /// Search products
        /// </summary>
        /// <param name="nameFilter">Name filter</param>
        /// <param name="seoFilter">SEO filter</param>
        /// <param name="categoryFilter">Category filter</param>
        /// <param name="manufacturerFilter">Manufacturer filter</param>
        /// <param name="priceFilter">Price filter</param>
        /// <param name="isPublished">Published filter</param>
        /// <returns>List of product entities</returns>
        IList<ProductModel> SearchProduct(
            string nameFilter = null,
            string seoFilter = null,
            string[] categoryFilter = null,
            string[] manufacturerFilter = null,
            string[] priceFilter = null,
            bool isPublished = true);

        /// <summary>
        /// Get product context table
        /// </summary>
        /// <returns></returns>
        IQueryable<ProductModel> Table();
    }
}
