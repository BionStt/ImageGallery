using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ImageGallery.Core.Interface.Service;
using ImageGallery.Core.Model;
using ImageGallery.Core.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace ImageGalley.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            var productEntities = _productService.GetAllProducts();
            var productList = new List<ProductListModel>();

            foreach (var product in productEntities)
            {
                var productModel = _mapper.Map<ProductModel, ProductListModel>(product);

                if (product.Images.Count > 0)
                {
                    // get first image
                    productModel.MainImage = product.Images
                        .OrderBy(x => x.SortOrder)
                        .ThenBy(x => x.Position)
                        .FirstOrDefault()
                        .Image
                        .FileName;
                }

                productList.Add(productModel);
            }

            return View(productList);
        }

        public IActionResult Create()
        {
            return View(new ProductCreateOrUpdateModel());
        }

        // POST: /Product/Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(List<Guid> ids)
        {
            if (ids == null || ids.Count == 0)
                return RedirectToAction("Index");

            _productService.DeleteProducts(ids);
            return RedirectToAction("Index");
        }

    }
}