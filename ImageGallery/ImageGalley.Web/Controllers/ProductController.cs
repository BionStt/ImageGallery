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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ProductCreateOrUpdateModel model, bool continueEditing)
        {
            var hasError = false;

            if (ModelState.IsValid)
            {
              
               
                    // generate new id for model
                    model.Id = Guid.NewGuid();

                    // map view model to entity
                    var productEntity = _mapper.Map<ProductCreateOrUpdateModel, ProductModel>(model);
                    productEntity.DateAdded = DateTime.Now;
                    productEntity.DateModified = DateTime.Now;

                    // save to database
                    _productService.InsertProduct(productEntity);
                    
                    SaveImageMappings(model);
                   

                    if (continueEditing)
                        return RedirectToAction("Edit", new { id = productEntity.Id, ActiveTab = model.ActiveTab });

                    return RedirectToAction("Index");
            }

            // something went wrong, redisplay form
            return View(model);
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


        private void SaveImageMappings(ProductCreateOrUpdateModel model)
        {
            var imageMappings = new List<ProductImageMappingModel>();
            if (model.ImageIds != null)
            {
                for (int i = 0; i < model.ImageIds.Count; i++)
                {
                    // convert sort order to int
                    int sortOrder = Convert.ToInt32(Math.Floor(Convert.ToDouble(model.ImageSortOrder[i])));

                    // check if image exist
                    Guid imageId;
                    if (Guid.TryParse(model.ImageIds[i], out imageId))
                    {
                        // create mapping entity
                        var imageMapping = new ProductImageMappingModel
                        {
                            Id = Guid.NewGuid(),
                            ProductId = model.Id,
                            ImageId = Guid.Parse(model.ImageIds[i]),
                            SortOrder = sortOrder,
                            Position = i
                        };

                        imageMappings.Add(imageMapping);
                    }
                }
            }

        }
}