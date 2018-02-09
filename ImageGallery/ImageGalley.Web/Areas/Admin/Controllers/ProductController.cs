using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ImageGallery.Core.Interface.Service;
using ImageGallery.Core.Model;
using ImageGalley.Web.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;

namespace ImageGalley.Web.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        public IMapper _mapper;
        private readonly IProductService _productService;

        public ProductController(IProductService productService, IMapper mapper)
        {
            _mapper = mapper;
            _productService = productService;
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
    }
}